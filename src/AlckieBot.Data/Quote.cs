using AlckieBot.Data.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data
{
    public static class Quote
    {
        public static void AddQuote(Model.Quote quote)
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/quotes?apiKey=" + MongoLabSettings.MongoLabAPIKey;

            var insertRequest = WebRequest.Create(url);
            insertRequest.ContentType = "application/json";
            insertRequest.Method = "POST";
            var insertBodyData = JsonConvert.SerializeObject(new List<Model.Quote> { quote });
            using (var streamWriter = new StreamWriter(insertRequest.GetRequestStream()))
            {
                streamWriter.Write(insertBodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var insertResponse = (HttpWebResponse)insertRequest.GetResponse();
        }

        public static List<Model.Quote> GetQuotes()
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/quotes?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<Model.Quote>>(responseData);

                return result;
            }

        }
    }
}
