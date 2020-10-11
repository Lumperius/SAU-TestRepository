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
using Hangfire;

namespace NewsUploader
{
    public class NewsService : INewsService
    {
        private readonly DataContext _context;
        private readonly IRssLoader _rssLoader;
        private readonly INewsParser _newsParser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsRater _newsRater;

        public NewsService(DataContext context, IUnitOfWork unitOfWork, INewsRater newsRater,
            IRssLoader rssLoader, INewsParser newsParser)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _rssLoader = rssLoader;
            _newsParser = newsParser;
            _newsRater = newsRater;
        }

        public async Task LoadNewsIntoDbFromRss(string url)
        {
            try
            {
                List<News> parsedNewsList = new List<News>();

                List<SyndicationItem> newsItems = _rssLoader.ReadRss(url); //Get rss feed
                foreach (SyndicationItem item in newsItems) //Add item's info to model
                {
                    if (_context.News.Any(n => n.Article == item.Title.Text) || item == null) { continue; }
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

        public async Task LoadAllNewsBody()
        {
            try
            {
                foreach (News news in await _unitOfWork.NewsRepository.GetAllAsync())
                {
                if (news == null && news.Body != null) { continue; }

                    if (news.Source.Contains("tut.by"))            //Check origin site
                        try { news.Body = _newsParser.TutByParseNews(news.Source); } catch { continue; }
                    if (news.Source.Contains("onliner.by"))
                        try { news.Body = _newsParser.OnlinerParseNews(news.Source); } catch { continue; }

                if(news.Body != null)
                    {
                        news.Body = HtmlCleaner.CleanHtml(news.Body);
                        news.PlainText = HtmlCleaner.GetPlainText(news.Body);
                        await _unitOfWork.SaveDBAsync();
                    }
                    else
                    {
                        Log.Information($"{DateTime.Now}|Info|Couldnt get body of {news.Source}");
                    }
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
                    if(news.PlainText == null && news.WordRating == 0) { continue; }
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

        public void StartHangfireForNews()
        {
            try
            {
                    RecurringJob.AddOrUpdate(
                        "TutBy",
                        () => this.LoadNewsIntoDbFromRss("https://news.tut.by/rss/all.rss"),
                         Cron.Hourly);
                    RecurringJob.AddOrUpdate(
                        "Onliner",
                        () => this.LoadNewsIntoDbFromRss("http://Onliner.by/feed"),
                         Cron.Hourly);
                    RecurringJob.AddOrUpdate(
                        "S13",
                        () => this.LoadNewsIntoDbFromRss("https://S13.ru/rss"),
                         Cron.Hourly);

                    RecurringJob.AddOrUpdate(
                        "BodyParser",
                        () => this.LoadAllNewsBody(),
                         "5 * * * *");

                    RecurringJob.AddOrUpdate(
                        "NewsRater",
                        () => this.RateNewsInDb(),
                        "*/15 * * * *");
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
