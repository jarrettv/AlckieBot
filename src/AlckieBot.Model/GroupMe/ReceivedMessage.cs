using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Model.GroupMe
{
    public class ReceivedMessage
    {
        public Attachment[] attachments { get; set; }
        public string avatar_url { get; set; }
        public string created_at { get; set; }
        public string group_id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string sender_id { get; set; }
        public string sender_type { get; set; }
        public string source_guid { get; set; }
        public bool system { get; set; }
        public string text { get; set; }
        public string user_id { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
