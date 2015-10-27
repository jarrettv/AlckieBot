using AlckieBot.Data.Base;
using AlckieBot.Model;
using AlckieBot.Model.GroupMe;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public class ClashCaller
    {
        public static string Code { get; set; }

        public static void Init()
        {
            var code = GetClashCallerCode();
            ClashCaller.Code = code ?? "";
        }

        public static string GetClashCallerCode()
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/clashcaller?apiKey=" + MongoLabSettings.MongoLabAPIKey + "&fo=true";
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);

                return result?.Code;
            }
        }

        public static void SetClashCaller(string code)
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/clashcaller?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            var deleteRequest = WebRequest.Create(url);
            deleteRequest.ContentType = "application/json";
            deleteRequest.Method = "PUT";
            var deleteBodyData = JsonConvert.SerializeObject(new List<Model.ClashCaller>());
            using (var streamWriter = new StreamWriter(deleteRequest.GetRequestStream()))
            {
                streamWriter.Write(deleteBodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var deleteResponse = (HttpWebResponse)deleteRequest.GetResponse();     
                   
            var insertRequest = WebRequest.Create(url);
            insertRequest.ContentType = "application/json";
            insertRequest.Method = "POST";
            var insertBodyData = JsonConvert.SerializeObject(new List<Model.ClashCaller> { new Model.ClashCaller { Code = code } });
            using (var streamWriter = new StreamWriter(insertRequest.GetRequestStream()))
            {
                streamWriter.Write(insertBodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var insertResponse = (HttpWebResponse)insertRequest.GetResponse();

            ClashCaller.Code = code;
        }
    }
}
