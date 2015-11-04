using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class WarCommands
    {
        public static List<Command> GetAllWarCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                ClashCallerCommand(bot),
                GetWarMatchUsCommand(bot),
                GetSetVillageCommand(bot),
                GetGetVillageCommand(bot)
            };
            return commands;
        }

        public static Command ClashCallerCommand(Bot bot)
        {
            return new Command("!clashcaller, !cc",
                               "Get the url for ClashCaller.",
                               "",
                               bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!CLASHCALLER" || message.text.ToUpper() == "!CC";
                               },
                               (message) =>
                               {
                                   if (ClashCaller.Code != "")
                                   {
                                       bot.SendMessage("http://www.clashcaller.com/war/" + ClashCaller.Code);
                                   }
                                   else
                                   {
                                       bot.SendMessage("I don't have the code atm.");
                                   }
                               });
        }

        public static Command GetWarMatchUsCommand(Bot bot)
        {
            return new Command("!warmatch, !wm", "Get the url for the current WarMatch.Us war.", "",
                               bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!WARMATCH" || message.text.ToUpper() == "!WM";
                               },
                               (message) =>
                               {
                                   bot.SendMessage("http://warmatch.us/wars/mist/");
                               });
        }

        public static Command GetSetVillageCommand(Bot bot)
        {
            return new Command("!setvillage #{village code}",
                               "Set your village code, so you can call targets using the !call command (Still under development).",
                               "!setvillage #00000000",
                               bot,
                               (message) =>
                               {
                                   var validate = Regex.Match(message.text.ToUpper(), "^!SETVILLAGE #([0-Z])+$", RegexOptions.IgnoreCase);
                                   return validate.Success;
                               },
                               (message) =>
                               {
                                   var villageCode = message.text.ToUpper().Substring("!SETVILLAGE ".Length);
                                   var member = Members.GetMemberByGroupMeID(message.sender_id);
                                   if (member == null)
                                   {
                                       member = new Model.Member
                                       {
                                           GroupMeID = message.sender_id,
                                           Name = message.name,
                                           Banned = false
                                       };
                                   }
                                   member.VillageCode = villageCode;
                                   Members.UpdateMember(member);
                                   bot.SendMessage($"Village code set to {villageCode}");
                               });
        }
        public static Command GetGetVillageCommand(Bot bot)
        {
            return new Command("!getvillage",
                               "Get your current village code.",
                               "",
                               bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!GETVILLAGE";
                               },
                               (message) =>
                               {
                                   try
                                   {
                                       var member = Members.GetMemberByGroupMeID(message.sender_id);
                                       if (member == null || String.IsNullOrEmpty(member.VillageCode))
                                       {
                                           bot.SendMessage("Your village is not set.");
                                       }
                                       else
                                       {
                                           bot.SendMessage($"Your village code is {member.VillageCode}");
                                       }
                                   }
                                   catch (Exception ex)
                                   {
                                       Bots.TestChatBot.SendMessage(ex.ToString());
                                   }
                               });
        }
    }
}
