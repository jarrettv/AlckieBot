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
    public static class Strike
    {
        public static void AddStrike(Model.Strike strike)
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/strikes?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            
            var insertRequest = WebRequest.Create(url);
            insertRequest.ContentType = "application/json";
            insertRequest.Method = "POST";
            var insertBodyData = JsonConvert.SerializeObject(new List<Model.Strike> { strike });
            using (var streamWriter = new StreamWriter(insertRequest.GetRequestStream()))
            {
                streamWriter.Write(insertBodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var insertResponse = (HttpWebResponse)insertRequest.GetResponse();
        }

        public static List<Model.Strike> GetStrikes()
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/strikes?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<Model.Strike>>(responseData);

                return result;
            }

        }

        public static List<Model.Strike> GetFakeStrikes()
        {
            var alckie = new Model.GroupMe.ChatMember { Name = "Alckie", UserID = "12345" };
            var siscim = new Model.GroupMe.ChatMember { Name = "Siscim", UserID = "22345" };
            var kermit = new Model.GroupMe.ChatMember { Name = "Kermit", UserID = "32345" };
            var papi = new Model.GroupMe.ChatMember { Name = "Papi Chulo", UserID = "44545" };
            var love = new Model.GroupMe.ChatMember { Name = "L0vesit", UserID = "23434" };
            return new List<Model.Strike>
            {
                new Model.Strike { Member = siscim, StrickenAt = DateTime.Now, Reason = "Being a bitch", StrickenBy = alckie },
                new Model.Strike { Member = love, StrickenAt = DateTime.Now, Reason = "Being a woman", StrickenBy = alckie },
                new Model.Strike { Member = papi, StrickenAt = DateTime.Now, Reason = "Sucking too much", StrickenBy = alckie },
                new Model.Strike { Member = alckie, StrickenAt = DateTime.Now, Reason = "Being himself", StrickenBy = alckie },
                new Model.Strike { Member = kermit, StrickenAt = DateTime.Now, Reason = "No reason", StrickenBy = alckie },
            };
        }
    }
}
