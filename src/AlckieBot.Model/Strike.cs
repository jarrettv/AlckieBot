using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Model
{
    public class Strike
    {
        public ChatMember Member { get; set; }
        public DateTime StrickenAt { get; set; }
        public string Reason { get; set; }
        public ChatMember StrickenBy { get; set; }
    }
}
