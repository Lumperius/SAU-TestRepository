﻿using NewsUploader.Interfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using RepositoryLibrary;
using System.Text;
using WorkingLibrary.DataContexts.WorkingUnit;
using ContextLibrary.DataContexts;
using ModelsLibrary;
using System.Threading.Tasks;
using System.Linq;
using ContextLibrary.Interfaces;
using RepositoryLibrary.RepositoryInterface;
using Serilog;

namespace NewsUploader
{
    public class NewsService : INewsService
    {
        private readonly DataContext _context;
        private readonly IRssLoader _rssLoader;
        private readonly INewsParser _newsParser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<string> urls = new List<string>()
            {
              "https://news.tut.by/rss/all.rss",
    //          "http://Onliner.by/feed",
    //          "https://S13.ru/rss"
            };


        public NewsService(DataContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _rssLoader = new RssLoader();
            _newsParser = new NewsParser();
        }



        public async Task LoadNewsInDb()
        {
            List<News> parsedNewsList = new List<News>();

            List<SyndicationItem> newsItems = _rssLoader.ReadRss(urls); //Get rss feed
            foreach(SyndicationItem item in newsItems) //Add item's info to model
            {
                if(_context.News.Any(n => n.ID.ToString() == item.Id.ToString()) || item == null) { continue; }
                News parsedNews = new News()
                {
                    ID = new Guid(),
                    Article = item.Title.Text,
                    DatePosted = item.LastUpdatedTime.DateTime,
                };

                //Check origin site
                if (item.Links.FirstOrDefault().Uri.Host.Contains("tut.by")) 
                    parsedNews.Body = _newsParser.TutByParseNews(item.Links.FirstOrDefault()?.Uri.AbsoluteUri);

                if (item.Links.FirstOrDefault().Uri.Host.Contains("onliner.by")) 
                    parsedNews.Body = _newsParser.OnlinerParseNews(item.Links.FirstOrDefault()?.Uri.AbsoluteUri);
                
                if (item.Links.FirstOrDefault().Uri.Host.Contains("S13.ru")) 
                    parsedNews.Body = _newsParser.S13ParseNews(item.Links.FirstOrDefault()?.Uri.AbsoluteUri);
                HtmlCleaner.CleanHtml(parsedNews.Body);
                if(parsedNews.Body != null)
                parsedNewsList.Add(parsedNews);  //Add model's object to list
            }


            await _unitOfWork.NewsRepository.Clear(); //Clear whole news database info
            await _unitOfWork.NewsRepository.AddRangeAsync(parsedNewsList); //Add list to database
            await _unitOfWork.SaveDBAsync(); //Save changes    
            
        }
    }

}
