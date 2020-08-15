using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NewsUploader.Interfaces;


namespace NewsUploader
{
    public class HtmlCleaner : IHtmlCleaner
    {
        public static string CleanHtml(string htmlText)
        {
           if(htmlText == null) { return null; }

            var d = Regex.Matches(htmlText, @"<img.+?>", RegexOptions.Singleline);

            htmlText = Regex.Replace(htmlText, @"<img.+?>", "", RegexOptions.Singleline);  //Remove images

            htmlText = Regex.Replace(htmlText, @"<!--SWF.+?>", "", RegexOptions.Singleline);  //Remove video

            var c = Regex.Matches(htmlText, @"<figure.+?figure>", RegexOptions.Singleline);
            htmlText = Regex.Replace(htmlText, @"<figure.+?figure>", "", RegexOptions.Singleline);  //Remove figures

            htmlText = Regex.Replace(htmlText, @"<script.+?</script>", "", RegexOptions.Singleline);  //Remove scripts

            htmlText = Regex.Replace(htmlText, @"<blockquote.+?>", "", RegexOptions.Singleline);
            htmlText = Regex.Replace(htmlText, @"<blockquote.+?blockquote>", "", RegexOptions.Singleline);  //Remove blockquotes                                                                                      //Remove blockquotes
            var b = Regex.Matches(htmlText, @"<blockquote.+?>", RegexOptions.Singleline);

            htmlText = Regex.Replace(htmlText, @"<a.+?>.+?</a>", "", RegexOptions.Singleline);  //Remove links

            htmlText = Regex.Replace(htmlText, @"<p>?</p>", "", RegexOptions.Singleline);  //Remove empty blocks


            return htmlText;
        }
    }
}
