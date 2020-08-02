using NewsUploader.Interfaces;
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

namespace NewsUploader
{
    public class NewsService : INewsService
    {
        private readonly NewsRepository _newsRepository;
        private readonly RssLoader _rssLoader;
        private readonly NewsParser _newsParser;
        private readonly WorkingUnit _workingUnit;
        public async Task LoadNewsInDb()
        {
            List<News> parsedNews = new List<News>();
            List<string> urls = new List<string>()
            {
              "https://news.tut.by/rss.html",
              "https://www.onliner.by/rss/news.rss",
              "https://s13.ru/rss"
            };

            List<SyndicationItem> newsItems = _rssLoader.ReadRss(urls);
            int i = 0;
            foreach(SyndicationItem item in newsItems)
            {
                parsedNews.Add(_newsParser.ParseNews(item.Links.FirstOrDefault()?.Uri.AbsoluteUri));
            }
            await _newsRepository.AddRangeAsync(parsedNews);
            await _workingUnit.SaveDBAsync();
            
        }
    }

}
