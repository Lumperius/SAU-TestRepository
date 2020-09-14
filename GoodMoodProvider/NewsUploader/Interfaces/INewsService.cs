using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace NewsUploader.Interfaces
{
    public interface INewsService
    {
        public Task LoadNewsInDb(string url);
        public Task GetAllNewsBody();
        public Task RateNewsInDb();
    }
}
