using Serilog;
using NewsUploader.Interfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Schema;
using System.Linq;

namespace NewsUploader
{
    public class RssLoader : IRssLoader
    {
        public RssLoader()
        {
        }

        public List<SyndicationItem> ReadRss( string siteRssUrl)
        {
            List<SyndicationItem> newsItems = new List<SyndicationItem>();
            try
            {
                using (XmlReader reader = XmlReader.Create(siteRssUrl))
                {
                    var feed = SyndicationFeed.Load(reader);
                    newsItems.AddRange(feed.Items);
                }
            }
            catch (Exception exRss)
            {
                Log.Error($"{DateTime.UtcNow} {exRss}");
                throw exRss;
            }
        
            return newsItems; 
        }
    }
}
