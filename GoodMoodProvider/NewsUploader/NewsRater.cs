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
                        "http://api.ispras.ru/texterra/v1/nlp?targetType=1emma&apikey=62808b2dcffffddb817320533e518b6f5e235c6f");
               
                    request.Content = new StringContent("[{\"text\":\"123\"}]",
                        Encoding.UTF8, "application/json");
               
                    var originWordsText = client.SendAsync(request).Result;
                    return await originWordsText.Content.ReadAsStringAsync(); ;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public double RateANews(string newsBody)
        {
            try
            {
                using (StreamReader reader = new StreamReader
                    ("C:/Users/Lenovo/Documents/GitHub/SAU-TestRepository/GoodMoodProvider/SideResourses/ASFINN-ru.json"))
                {
                    string json = reader.ReadToEnd();
                    IDictionary<string, int> items = JsonConvert.DeserializeObject<IDictionary<string, int>>(json);

                    string[] originWords = newsBody.Split(" ");

                    int totalValue = 0;

                    foreach (string word in originWords)
                    {
                        totalValue += items.Where(i => i.Key == word).FirstOrDefault().Value;
                    }
                    double relativeValue = totalValue / items.Count;
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
