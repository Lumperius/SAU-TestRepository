using HtmlAgilityPack;
using ModelsLibrary;
using NewsUploader.Interfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace NewsUploader
{
    public class NewsParser : INewsParser
    {
        public News ParseNews(string newsUrl)
        {
            News parsedNews = new News();

            var web = new HtmlWeb();
            var doc = web.Load(newsUrl);
            var docNode = doc.DocumentNode;
            var content = docNode.SelectSingleNode("@id='utm_article_block'");
            return parsedNews;
        }
    }
}
