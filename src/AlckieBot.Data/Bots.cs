using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data
{
    public static class Bots
    {
        public static Bot LeadershipChatBot { get; set; }
        public static Bot GeneralChatBot { get; set; }
        public static Bot WarChatBot { get; set; }
        public static Bot TestChatBot { get; set; }

        public static Bot WhenInRomeChatBot { get; set; }

        public static Bot ReconChatBot { get; set; }

        public static void Init()
        {
            var bots = Bot.GetRegisteredBots(ConfigurationManager.AppSettings["GROUPME_TOKEN"]);
            var botName = ConfigurationManager.AppSettings["BOT_NAME"];

            LeadershipChatBot = bots.FirstOrDefault(b => b.GroupID == ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"] && b.Name == botName);
            if (LeadershipChatBot != null)
            {
                LeadershipChatBot.GroupName = "leadership";
                LeadershipChatBot.CanCallMods = true;
            }

            GeneralChatBot = bots.FirstOrDefault(b => b.GroupID == ConfigurationManager.AppSettings["GENERALCHAT_ID"] && b.Name == botName);
            if (GeneralChatBot != null)
            {
                GeneralChatBot.GroupName = "general";
            }

            WarChatBot = bots.FirstOrDefault(b => b.GroupID == ConfigurationManager.AppSettings["WARCHAT_ID"] && b.Name == botName);
            if (WarChatBot != null)
            {
                WarChatBot.CanCallMods = true;
                WarChatBot.GroupName = "war";
            }

            TestChatBot = bots.FirstOrDefault(b => b.GroupID == ConfigurationManager.AppSettings["TESTCHAT_ID"] && b.Name == botName);
            if (TestChatBot != null)
            {
                TestChatBot.GroupName = "test";
            }

            WhenInRomeChatBot = bots.FirstOrDefault(b => b.GroupID == ConfigurationManager.AppSettings["WIRCHAT_ID"] && b.Name == botName);
            if (WhenInRomeChatBot != null)
            {
                WhenInRomeChatBot.GroupName = "wheninrome";
            }

            ReconChatBot = bots.FirstOrDefault(b => b.GroupID == "11791099" && b.Name == botName);
            if (ReconChatBot != null)
            {
                ReconChatBot.GroupName = "recon";
            }
        }
    }
}
