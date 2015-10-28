using AlckieBot.Data;
using AlckieBot.Helpers;
using AlckieBot.Model.GroupMe;
using Newtonsoft.Json;
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
                GetTagLoganCommand(bot),
                GetStrikeListCommand(bot),
                GetStrikeCommand(bot)
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

        public static Command GetStrikeCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   var isStrikeCommand = (message.text.ToUpper().StartsWith("!STRIKE "));
                                   if (!isStrikeCommand || !Mods.AllMods.Contains(message.sender_id))
                                   {
                                       //Return earlier 
                                       return false;
                                   }
                                   var containsAttachment = message.attachments?.Length == 1;
                                   var isAMention = message.attachments[0]?.Type == "mentions";
                                   var containsOnlyOneMention = message.attachments[0]?.User_ids?.Length == 1;

                                   return containsAttachment && isAMention && containsOnlyOneMention;
                               },
                               (message) =>
                               {
                                   var mention = message.attachments[0];

                                   var strikeReason = message.text.Substring(mention.Loci[0][0] + mention.Loci[0][1] + 1);
                                   var strickenUserName = message.text.Substring(mention.Loci[0][0] + 1, mention.Loci[0][1] - 1);
                                   var strickenUserID = mention.User_ids[0];
                                   var strikeMessage = $"{strickenUserName} was issued a strike for: {strikeReason}";


                                   if (mention.Loci[0][0] != "!SAVEQUOTE ".Length || strikeReason.Length == 0)
                                   {
                                       bot.SendMessage("The way to use this command is \"!strike <tag> <reason>\".");
                                   }
                                   else
                                   {
                                       Strike.AddStrike(new Model.Strike
                                       {
                                           Member = new ChatMember
                                           {
                                               Name = strickenUserName,
                                               UserID = strickenUserID
                                           },
                                           StrickenAt = DateTime.UtcNow,
                                           Reason = strikeReason,
                                           StrickenBy = new ChatMember
                                           {
                                               Name = message.name,
                                               UserID = message.sender_id
                                           }
                                       });

                                       bot.SendMessage(strikeMessage);
                                   }
                               });
        }

        public static Command GetStrikeListCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!STRIKELIST";
                               },
                               (message) =>
                               {
                                   bot.SendMessage("http://alckiebot.azurewebsites.net/strikes");
                                   //var strikes = Strike.GetStrikes();
                                   //var sb = new StringBuilder();
                                   //foreach(var strike in strikes)
                                   //{
                                   //    string timeAgo;
                                   //    var dateDiff = DateTime.UtcNow.Subtract(strike.StrickenAt);
                                   //    if (dateDiff.Days > 7)
                                   //    {
                                   //        var weeks = dateDiff.Days / 7;
                                   //        var days = dateDiff.Days % 7;
                                   //        timeAgo = $"{weeks}w{days}d ago";
                                   //    }
                                   //    else if (dateDiff.Days > 0)
                                   //    {
                                   //        timeAgo = $"{dateDiff.Days}d ago";
                                   //    }
                                   //    else
                                   //    {
                                   //        timeAgo = "today";
                                   //    }
                                   //    sb.AppendLine($"- {strike.Member.Name} {timeAgo} for {strike.Reason}");
                                   //}
                                   //bot.SendMessage(sb.ToString());
                               });
        }
    }
}
