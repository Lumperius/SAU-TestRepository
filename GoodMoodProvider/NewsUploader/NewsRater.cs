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
                        "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=62808b2dcffffddb817320533e518b6f5e235c6f");
               
                    request.Content = new StringContent($"[{{\"text\":\"{targetNews.PlainText}\"}}]",
                        Encoding.UTF8, "application/json");
                    var a = request.ToString();
                    var requestResult = client.SendAsync(request).Result;
                    if (requestResult != null)
                    {
                        return await requestResult.Content.ReadAsStringAsync(); ;
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


        public double RateANews(string newsBody )
        {
            try
            {
                using (StreamReader reader = new StreamReader
                    ("../SideResources/AFINN-ru.json"))
                {
                    string[] originWords = newsBody.Split(" ");
                    string text = reader.ReadToEnd();

                    JsonSerializer serializer = new JsonSerializer();
                    var props = (serializer.Deserialize( new JsonTextReader(new StringReader(text))) as JObject).Properties();
                   
                    int totalValue = 0;
                    foreach (string word in originWords)
                    {
                        var value = (props.FirstOrDefault(p => p.Name == word)?.Value);
                       
                        int itemValue = 0;
                        if (value != null)
                            if (int.TryParse(value.ToString(), out itemValue)) //Checks if it's int convertable value
                            {
                                totalValue += itemValue;
                            }
                            else
                            {
                                continue;
                            }
                    }
                    double relativeValue = totalValue / originWords.Length;
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
