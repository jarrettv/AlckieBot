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
    public static class Village
    {
        public static void SetVillage(Model.Village village)
        {
            var query = JsonConvert.SerializeObject(new
            {
                GroupMeID = village.GroupMeID
            });
            var url = $"{MongoLabSettings.MONGOLAB_API}databases/alckiebot/collections/villages?apiKey={MongoLabSettings.MongoLabAPIKey}&query={WebUtility.UrlEncode(query)}&u=true";

            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "PUT";
            var bodyData = JsonConvert.SerializeObject(village);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(bodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        public static List<Model.Village> GetVillages()
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/villages?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<Model.Village>>(responseData);

                return result;
            }

        }

        public static Model.Village GetVillageByUserID(string userID)
        {
            var query = JsonConvert.SerializeObject(new
            {
                GroupMeID = userID
            });
            var url = $"{MongoLabSettings.MONGOLAB_API}databases/alckiebot/collections/villages?apiKey={MongoLabSettings.MongoLabAPIKey}&query={WebUtility.UrlEncode(query)}&fo=true";
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Model.Village>(responseData);

                return result;
            }
        }
    }
}
