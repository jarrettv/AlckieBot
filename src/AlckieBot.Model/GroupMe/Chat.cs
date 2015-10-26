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
    public class Chat
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<ChatMember> Members { get; set; }

        public static List<Chat> GetAllGroups(string groupMeToken)
        {
            var groups = new List<Chat>();
            var request = WebRequest.Create(API.URL + "groups?token=" + groupMeToken);
            request.ContentType = "text/json";
            request.Method = "GET";

            var bots = new List<Bot>();
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseData = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);
                if (result.response != null && result.response.Count > 0)
                {
                    foreach(var group in result.response)
                    {
                        var groupMembers = new List<ChatMember>();
                        foreach(var member in group.members)
                        {
                            groupMembers.Add(new ChatMember
                            {
                                UserID = member.user_id,
                                Name = member.nickname
                            });
                        }
                        groups.Add(new Chat
                        {
                            ID = group.id,
                            Name = group.name,
                            Members = groupMembers
                        });
                    }
                }
                return groups;
            }
        }
    }
}
