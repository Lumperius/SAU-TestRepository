using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsUploader.Interfaces
{
    public interface INewsParser
    {
        public string TutByParseNews(string url);
        public string OnlinerParseNews(string newsUrl);
        public string S13ParseNews(string newsUrl);
    }
}
