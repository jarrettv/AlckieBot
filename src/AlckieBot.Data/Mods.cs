using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data
{
    public static class Mods
    {
        public static string Alckie
        {
            get
            {
                return ConfigurationManager.AppSettings["ALCKIE_ID"];
            }

        }
        public static string Siscim
        {
            get
            {
                return ConfigurationManager.AppSettings["SISCIM_ID"];
            }

        }

        public static List<string> AllMods { get; private set; }
        public static void Init()
        {
            var leadershipGroup = Chat.GetAllGroups(ConfigurationManager.AppSettings["GROUPME_TOKEN"]).FirstOrDefault(g => g.ID == ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"]);
            if (leadershipGroup != null)
            {
                AllMods = leadershipGroup.Members.Select(m => m.UserID).ToList();
            }
        }
        public static bool IsUserAMod(string userID)
        {
            return AllMods.Contains(userID);
        }
    }
}
