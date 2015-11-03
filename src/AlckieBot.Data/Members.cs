using AlckieBot.Data.Base;
using AlckieBot.Model.GroupMe;
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
    public static class Members
    {
        public static void UpdateMember(Model.Member member)
        {
            var query = JsonConvert.SerializeObject(new
            {
                GroupMeID = member.GroupMeID
            });
            var url = $"{MongoLabSettings.MONGOLAB_API}databases/alckiebot/collections/members?apiKey={MongoLabSettings.MongoLabAPIKey}&q={WebUtility.UrlEncode(query)}&u=true";

            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "PUT";
            var bodyData = JsonConvert.SerializeObject(member);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(bodyData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        public static List<Model.Member> GetMembers()
        {
            var url = MongoLabSettings.MONGOLAB_API + "databases/alckiebot/collections/members?apiKey=" + MongoLabSettings.MongoLabAPIKey;
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<Model.Member>>(responseData);

                return result;
            }

        }

        public static Model.Member GetMemberByGroupMeID(string groupMeID)
        {
            var query = JsonConvert.SerializeObject(new
            {
                GroupMeID = groupMeID
            });
            var url = $"{MongoLabSettings.MONGOLAB_API}databases/alckiebot/collections/members?apiKey={MongoLabSettings.MongoLabAPIKey}&q={WebUtility.UrlEncode(query)}&fo=true";
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Model.Member>(responseData);

                return result;
            }
        }

        public static void UpdateMembersByGroup(string groupMeToken, string groupID)
        {
            var dbMembers = GetMembers();
            var chat = Model.GroupMe.Chat.GetGroup(groupMeToken, groupID);
            if (chat != null)
            {
                foreach (var member in chat.Members)
                {
                    var dbMember = dbMembers.FirstOrDefault(m => m.GroupMeID == member.UserID);
                    if (dbMember == null)
                    {
                        UpdateMember(new Model.Member
                        {
                            GroupMeID = member.UserID,
                            Name = member.Name,
                            VillageCode = "",
                            Banned = false
                        });
                    }
                }
            }
        }
        public static void UpdateMembersByGroupAndKickIfBanned(string groupMeToken, string groupID, Bot bot)
        {
            var dbMembers = GetMembers();
            var chat = Model.GroupMe.Chat.GetGroup(groupMeToken, groupID);
            if (chat != null)
            {
                foreach (var member in chat.Members)
                {
                    var dbMember = dbMembers.FirstOrDefault(m => m.GroupMeID == member.UserID);
                    if (dbMember == null)
                    {
                        UpdateMember(new Model.Member
                        {
                            GroupMeID = member.UserID,
                            Name = member.Name,
                            VillageCode = "",
                            Banned = false
                        });
                    }
                    else if (dbMember.Banned)
                    {
                        bot.KickUser(groupMeToken, member.UserID);
                        bot.SendMessage("GTFO!");
                    }
                }
            }
        }
    }
}
