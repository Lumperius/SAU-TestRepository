using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsUploader.Interfaces
{
    public interface INewsRater
    {
        public double RateANews(string newsBody);
        public Task<string> SimplifyANews(News targetNews);

    }
}
