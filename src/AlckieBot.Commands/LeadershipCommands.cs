using AlckieBot.Data;
using AlckieBot.Helpers;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class LeadershipCommands
    {
        public static List<Command> GetAllLeadershipCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                GetCommandsCommand(bot),
                GetSetCCCommand(bot),
                GetTagLoganCommand(bot)
            };
            return commands;
        }

        public static Command GetCommandsCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.ToUpper() == "!COMMANDS");
            },
            (message) =>
            {
                bot.SendMessage("http://alckiebot.azurewebsites.net/commands/leadership");
            });
        }
        public static Command GetTagLoganCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.ToUpper() == "!TAGLOGAN");
            },
            (message) =>
            {
                var pleb = Pleb.Logan;
                if (message.sender_id == Pleb.BMan.ID)
                {
                    pleb = Pleb.BMan;
                }
                var attachments = new List<dynamic>();
                attachments.Add(new
                {
                    loci = new int[][]
                    {
                            new int[]
                            {
                                0,
                                pleb.Name.Length + 1
                            }
                    },
                    type = "mentions",
                    user_ids = new string[]
                     {
                            pleb.ID
                     }
                });
                Bots.GeneralChatBot.SendMessage("@" + pleb.Name + "", attachments);
            });
        }

        public static Command GetSetCCCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.text.ToUpper().StartsWith("!SETCC ");
                               },
                               (message) =>
                               {
                                   var clashCallerCode = message.text.Substring(7);
                                   ClashCaller.SetClashCaller(clashCallerCode);
                                   bot.SendMessage("Clash caller code set to " + clashCallerCode);
                               });
        }
    }
}
