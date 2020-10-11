using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace NewsUploader.Interfaces
{
    public interface INewsService
    {
        public Task LoadNewsIntoDbFromRss(string url);
        public Task LoadAllNewsBody();
        public Task RateNewsInDb();
        public void StartHangfireForNews();
    }
}
