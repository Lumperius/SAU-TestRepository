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
using System.Net.Http.Headers;

namespace NewsUploader
{
    public class NewsRater : INewsRater
    {
        public async Task<string> SimplifyANews(News targetNews)
        {
            try
            {
                string response;
                using (var client = new HttpClient()) //Create http client
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers //Constructing request
                        .MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                        "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=e2fdf1d8ad55d95c9185543b3c6547491cc131f8");
                    request.Content = new StringContent($"[{{\"text\":\"{targetNews.PlainText}\"}}]",
                        Encoding.UTF8, "application/json");
                    var requestResult = await client.SendAsync(request);  //Sending request
                    response = await requestResult.Content.ReadAsStringAsync(); // Getting body of response as string 
                }
                    //Remove everything from responce body leaving just text
                    var matches = Regex.Matches(response, "\"value\":\".+?\""); //Get matches of word values
                        string lemmatizedText = "";
                        foreach(Match match in matches)
                        {
                            string matchValue = match.Value;
                            matchValue = Regex.Replace(matchValue, "\"value\":\"", ""); //Deleting everything exceot the word 
                            matchValue = Regex.Replace(matchValue, "\"", "");           //and adding space symbol
                            matchValue = Regex.Replace(matchValue, "}", "");
                            matchValue = Regex.Replace(matchValue, "{", "");
                            matchValue = Regex.Replace(matchValue, ",", "");
                            lemmatizedText += matchValue;  //Merge words into text
                            lemmatizedText += " ";
                    }
                    if (lemmatizedText != null && lemmatizedText.Length > 10)  //Check state of collected text
                        {
                            return lemmatizedText;
                        }
                        else
                        {
                            return targetNews.PlainText;
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
                    string text = afinnReader.ReadToEnd();  //Getting string from jsom file

                    JsonSerializer serializer = new JsonSerializer();
                    var afinnProps = (serializer.Deserialize
                        (new JsonTextReader(new StringReader(text))) as JObject).Properties(); //Getting list of properties

                        string[] originWords = targetText.Split(" "); //Split target text into array of words

                        int valuedWordsCount = 0; //Number of valued words
                        int totalValue = 0; //Summ of all values
                        int relativeValue = 0; //Relative value for whole text

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
                         relativeValue = (int)totalValue * 10 / valuedWordsCount;//Getting relative value(*10 so it could stay int)
                        if (relativeValue == 0) { relativeValue++; }
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
