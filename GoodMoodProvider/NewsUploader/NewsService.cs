using NewsUploader.Interfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using RepositoryLibrary;
using System.Text;
using ContextLibrary.DataContexts;
using ModelsLibrary;
using System.Threading.Tasks;
using System.Linq;
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
        private readonly INewsRater _newsRater;
        public readonly List<string> urls = new List<string>()
            {
              "https://news.tut.by/rss/all.rss",
    //          "http://Onliner.by/feed",
    //          "https://S13.ru/rss"
            };

        public NewsService(DataContext context, IUnitOfWork unitOfWork, INewsRater newsRater,
            IRssLoader rssLoader, INewsParser newsParser)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _rssLoader = rssLoader;
            _newsParser = newsParser;
            _newsRater = newsRater;
        }

        public async Task LoadNewsInDb(string url)
        {
            try
            {
                List<News> parsedNewsList = new List<News>();

                List<SyndicationItem> newsItems = _rssLoader.ReadRss(url); //Get rss feed
                foreach (SyndicationItem item in newsItems) //Add item's info to model
                {
                    if (_context.News.Any(n => n.ID.ToString() == item.Id.ToString()) || item == null) { continue; }
                    News parsedNews = new News()
                    {
                        ID = new Guid(),
                        Article = item.Title.Text,
                        DatePosted = item.LastUpdatedTime.DateTime,
                        Source = item.Links.FirstOrDefault()?.Uri.AbsoluteUri
                    };
                parsedNewsList.Add(parsedNews);  //Add model's object to list
                }
                await _unitOfWork.NewsRepository.AddRangeAsync(parsedNewsList); //Add list to database
                await _unitOfWork.SaveDBAsync(); //Save changes             
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetAllNewsBody()
        {
            try
            {
                foreach (News news in await _unitOfWork.NewsRepository.GetAllAsync())
                {
                if (news == null && news.Body != null) { continue; } 
                    
                if (news.Source.Contains("tut.by"))            //Check origin site
                    news.Body = _newsParser.TutByParseNews(news.Source);
                if (news.Source.Contains("onliner.by"))      
                    news.Body = _newsParser.OnlinerParseNews(news.Source);
                if (news.Source.Contains("S13.ru"))           
                    news.Body = _newsParser.S13ParseNews(news.Source);


                news.Body = HtmlCleaner.CleanHtml(news.Body);
                news.PlainText = HtmlCleaner.GetPlainText(news.Body);
                await _unitOfWork.SaveDBAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task RateNewsInDb()
        {
            try
            {
                var newsList = await _unitOfWork.NewsRepository.GetAllAsync();

                foreach (News news in newsList)
                {
                    if(news.PlainText == null) { continue; }
                    string simplifiedText = await _newsRater.SimplifyANews(news);
                    news.WordRating = _newsRater.RateANews(simplifiedText);
                    await _unitOfWork.SaveDBAsync();
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
