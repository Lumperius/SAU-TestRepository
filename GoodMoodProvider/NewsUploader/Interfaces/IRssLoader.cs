using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace NewsUploader.Interfaces
{
    public interface IRssLoader
    {
        public List<SyndicationItem> ReadRss(List<string> urls);

    }
}
