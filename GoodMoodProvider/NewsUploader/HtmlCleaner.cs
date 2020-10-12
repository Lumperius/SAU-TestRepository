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
            try
            {
                if (htmlText == null) { return null; }

                htmlText = Regex.Replace(htmlText, @"<img.+?>", "", RegexOptions.Singleline);  //Remove images

                htmlText = Regex.Replace(htmlText, @"<!--SWF.+?>", "", RegexOptions.Singleline);  //Remove video

                htmlText = Regex.Replace(htmlText, @"<figure.+?figure>", "", RegexOptions.Singleline);  //Remove figures

                htmlText = Regex.Replace(htmlText, @"<script.+?</script>", "", RegexOptions.Singleline);  //Remove scripts

                htmlText = Regex.Replace(htmlText, @"<blockquote.+?>", "", RegexOptions.Singleline);
                htmlText = Regex.Replace(htmlText, @"<blockquote.+?blockquote>", "", RegexOptions.Singleline);  //Remove blockquotes                                                                                      //Remove blockquotes

                htmlText = Regex.Replace(htmlText, @"<a.+?>", "", RegexOptions.Singleline);  //Remove links
                htmlText = Regex.Replace(htmlText, @"</a>", "", RegexOptions.Singleline);  //Remove links

                htmlText = Regex.Replace(htmlText, @"<p>?</p>", "", RegexOptions.Singleline);  //Remove empty blocks

                htmlText = Regex.Replace(htmlText, @"\n{2,}", " ", RegexOptions.Singleline);  //Remove empty strings

                htmlText = Regex.Replace(htmlText, @"\s{3,}", " ", RegexOptions.Singleline);  //Remove whitespaces

                htmlText = Regex.Replace(htmlText, @"\s{3,}", " ", RegexOptions.Singleline);  //Remove whitespaces

                return htmlText;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string GetPlainText(string htmlText)
        {
            try
            {
                htmlText = Regex.Replace(htmlText, @"<.+?>", "", RegexOptions.Singleline);  //Remove all tags
                htmlText = Regex.Replace(htmlText, @"&.+?;", "", RegexOptions.Singleline); // Remove garbage
                htmlText = Regex.Replace(htmlText, @"\""", "", RegexOptions.Singleline);  //Remove quotes
                htmlText = Regex.Replace(htmlText, @"«", "", RegexOptions.Singleline);  //Remove quotes
                htmlText = Regex.Replace(htmlText, @"»", "", RegexOptions.Singleline);  //Remove quotes
                htmlText = Regex.Replace(htmlText, @"\n", "", RegexOptions.Singleline);  //Remove special symbols
                htmlText = Regex.Replace(htmlText, @"\.", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"%", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"[0-9]+?", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"[a-z]+?", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"[A-Z]+?", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @",", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"\[", "", RegexOptions.Singleline); //
                htmlText = Regex.Replace(htmlText, @"]", "", RegexOptions.Singleline); //

                return htmlText;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
