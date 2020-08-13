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

        public List<SyndicationItem> ReadRss( List<string> siteUrls)
        {
            List<SyndicationItem> newsItems = new List<SyndicationItem>();
            try
            {
                foreach (string url in siteUrls)
                {
                    using (XmlReader reader = XmlReader.Create(url))
                    {
                        var feed = SyndicationFeed.Load(reader);
                        newsItems.AddRange(feed.Items);
                    }
                }
            }
            catch (Exception exRss)
            {
                Log.Error($"{DateTime.UtcNow} {exRss}");
            }
        
            return newsItems; 
        }
    }
}
