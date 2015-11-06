using AlckieBot.Model.Giphy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Model.GroupMe
{
    public class Bot
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string GroupID { get; private set; }
        public string GroupName { get; set; }
        public bool CanSpeak { get; set; }
        public bool IsBeingADouche { get; set; }
        public bool CanCallMods { get; set; }
        public List<GiphySpamCounter> GifCounters { get; set; }
        public Dictionary<string, int> CustomCounters { get; set; }

        private Bot(string id, string name, string groupID)
        {
            this.ID = id;
            this.Name = name;
            this.GroupID = groupID;
            this.CanSpeak = true;
            this.IsBeingADouche = false;
            this.CanCallMods = false;
            this.GifCounters = new List<GiphySpamCounter>();
            this.CustomCounters = new Dictionary<string, int>();
        }

        public static Bot Register(string groupMeToken, string name, string groupID)
        {
            var request = WebRequest.Create(API.URL + "bots?token=" + groupMeToken);
            request.ContentType = "text/json";
            request.Method = "POST";
            var bodyData = JsonConvert.SerializeObject(new
            {
                bot = new
                {
                    name = name,
                    group_id = groupID
                }
            });
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(bodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);

                return new Bot(result.response.bot.bot_id.ToString(), name, groupID);
            }
        }

        public static List<Bot> GetRegisteredBots(string groupMeToken)
        {
            var request = WebRequest.Create(API.URL + "bots?token=" + groupMeToken);
            request.ContentType = "text/json";
            request.Method = "GET";

            var bots = new List<Bot>();
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);
                foreach (var bot in result.response)
                {
                    bots.Add(new Bot(bot.bot_id.ToString(), bot.name.ToString(), bot.group_id.ToString()));
                }
            }
            return bots;
        }

        public void SendMessage(string text, object attachments = null)
        {
            var request = WebRequest.Create(API.URL + "bots/post");
            request.ContentType = "text/json";
            request.Method = "POST";
            dynamic bodyData = new
            {
                bot_id = this.ID,
                text = text,
                attachments = attachments
            };
            var requestData = JsonConvert.SerializeObject(bodyData);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public bool KickUser(string groupMeToken, string userID)
        {
            var chat = Chat.GetGroup(groupMeToken, this.GroupID);
            if (chat == null)
            {
                return false;
            }
            var member = chat.Members.FirstOrDefault(m => m.UserID == userID);
            if (member == null)
            {
                return false;
            }
            var memberID = member.ID;
            var url = $"{API.URL}groups/{this.GroupID.Trim()}/members/{memberID.Trim()}/remove?token={groupMeToken}";
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var response = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return response.StatusCode == HttpStatusCode.OK;
        }

        public GiphySpamCounter GetUserGifSpamCounter(string userId)
        {
            var userSpamCounter = this.GifCounters.FirstOrDefault(c => c.UserID == userId);
            if (userSpamCounter == null)
            {
                userSpamCounter = new GiphySpamCounter { UserID = userId, GifCounter = 0, HasBeenWarned = false };
                this.GifCounters.Add(userSpamCounter);
            }
            return userSpamCounter;
        }

        public override bool Equals(object obj)
        {
            if (this == null || obj == null || obj.GetType() != typeof(Bot))
                return false;

            return this.ID == ((Bot)obj).ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
