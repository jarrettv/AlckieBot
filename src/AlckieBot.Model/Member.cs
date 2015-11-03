using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlckieBot.Model.GroupMe;

namespace AlckieBot.Model
{
    public class Member
    {
        public string GroupMeID { get; set; }
        public string Name { get; set; }
        public string VillageCode { get; set; }
        public bool Banned { get; set; }
    }
}
