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
            try
            {
                var leadershipGroup = Chat.GetGroup(ConfigurationManager.AppSettings["GROUPME_TOKEN"], ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"]);
                if (leadershipGroup != null)
                {
                    //Everyone on mod chat, but Mike is considered a mod.
                    AllMods = leadershipGroup.Members.Where(m => m.UserID != "22620347").Select(m => m.UserID).ToList();
                }
            }
            //Sometimes GroupMe API goes down, so it's better to guarantee that someone can shut down the bot.
            catch(Exception)
            {
                AllMods = new List<string>
                {
                    Alckie,
                    Siscim
                };
            }
        }
        public static bool IsUserAMod(string userID)
        {
            return AllMods.Contains(userID);
        }
    }
}
