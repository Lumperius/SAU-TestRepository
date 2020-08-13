using HtmlAgilityPack;
using ModelsLibrary;
using NewsUploader.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;

namespace NewsUploader
{
    public class NewsParser : INewsParser
    {
        public NewsParser()
        {
        }

        public string TutByParseNews(string newsUrl)
        {

            var web = new HtmlWeb();
            var doc = web.Load(newsUrl);
            var docNode = doc.DocumentNode;
            var listContent = docNode.Descendants("div")
                .Where(d => d.Id == "article_body")
                .FirstOrDefault()?
                .InnerHtml;

            return listContent;
        }


        public string OnlinerParseNews(string newsUrl)
        {
            var web = new HtmlWeb();
            var doc = web.Load(newsUrl);
            var docNode = doc.DocumentNode;
            var listContent = docNode.Descendants()
                .Where(d => d.Name == "div")
                .Where(d => d.Attributes.FirstOrDefault().Name == "class")
                .Where(d => d.Attributes.FirstOrDefault().Value == "news-text")
                .FirstOrDefault()?
                .InnerHtml;

            return listContent;
        }

        public string S13ParseNews(string newsUrl)
        {

            var web = new HtmlWeb();
            var doc = web.Load(newsUrl);
            var docNode = doc.DocumentNode;
            var listContent = docNode.Descendants("div")
                .Where(d => d.Attributes["class"].Value == "adcd1194b61eedc58d59107552c8674a1 b880x90")
                .FirstOrDefault()?
                .InnerHtml;

            return listContent;
        }


    }
}
