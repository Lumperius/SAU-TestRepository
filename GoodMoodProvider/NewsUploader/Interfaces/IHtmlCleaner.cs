using System;
using System.Collections.Generic;
using System.Text;

namespace NewsUploader.Interfaces
{
    public interface IHtmlCleaner
    {
        public static string CleanHtml(string htmlText)
        {
            return htmlText;
        }
    }
}
