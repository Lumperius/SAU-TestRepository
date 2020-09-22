using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewsUploader.Interfaces;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace NewsUploader
{
    public class NewsRater : INewsRater
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsRater(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SimplifyANews(News targetNews)
        {
            try
            { 
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
               
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                        "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=e2fdf1d8ad55d95c9185543b3c6547491cc131f8");
               
                    request.Content = new StringContent($"[{{\"text\":\"{targetNews.PlainText}\"}}]",
                        Encoding.UTF8, "application/json");
                    var requestResult = client.SendAsync(request).Result;
                    
                    var response = await requestResult.Content.ReadAsStringAsync();


                    //Remove everything from responce bode aside from lemma tokens

                    var matches = Regex.Matches(response, "\"value\":\".+?\"");
                        string lemmatizedText = "";
                        foreach(Match match in matches)
                        {
                            string matchValue = match.Value;
                            matchValue = Regex.Replace(matchValue, "\"value\":\"", "");
                            matchValue = Regex.Replace(matchValue, "\"", "");
                            matchValue = Regex.Replace(matchValue, "}", "");
                            matchValue = Regex.Replace(matchValue, "{", "");
                            matchValue = Regex.Replace(matchValue, ",", "");
                            lemmatizedText += matchValue;
                            lemmatizedText += " ";
                    }
                    if (lemmatizedText != null || lemmatizedText != "")
                        {
                            return lemmatizedText;
                        }
                        else
                        {
                            return targetNews.PlainText;
                        }
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public double RateANews(string targetText)
        {
            try
            {
                using (StreamReader afinnReader = new StreamReader
                    ("../SideResources/AFINN-ru.json"))
                {
                    string text = afinnReader.ReadToEnd();

                    JsonSerializer serializer = new JsonSerializer();
                    var afinnProps = (serializer.Deserialize
                        (new JsonTextReader(new StringReader(text))) as JObject).Properties();

                        string[] originWords = targetText.Split(" ");

                        int valuedWordsCount = 0;
                        int totalValue = 0;
                        int relativeValue = 0;

                        foreach (string word in originWords)
                        {
                            var value = (afinnProps.FirstOrDefault(p => p.Name == word)?.Value);

                            int itemValue = 0;
                            if (value != null)
                                if (int.TryParse(value.ToString(), out itemValue)) //Checks if it's int convertable value
                                {
                                    valuedWordsCount++;
                                    totalValue += itemValue;
                                }
                                else
                                {
                                    continue;
                                }
                        }

                        if(valuedWordsCount != 0)
                    {
                         relativeValue = (int)totalValue * 100 / valuedWordsCount;
                    }
                    return relativeValue;

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
