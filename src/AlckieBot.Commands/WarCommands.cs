using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class WarCommands
    {
        public static List<Command> GetAllWarCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                GetCommandsCommand(bot),
                GetClashCallerCommand(bot),
                GetBetaClashCallerCommand(bot)
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
                bot.SendMessage("http://alckiebot.azurewebsites.net/commands/war");
            });
        }

        public static Command GetClashCallerCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!CLASHCALLER" || message.text.ToUpper() == "!CC";
                               },
                               (message) =>
                               {
                                   if (StaticData.ClashCallerCode != "")
                                   {
                                       bot.SendMessage("http://www.clashcaller.com/war/" + StaticData.ClashCallerCode);
                                   }
                                   else
                                   {
                                       bot.SendMessage("I don't have the code atm.");
                                   }
                               });
        }
        public static Command GetBetaClashCallerCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "!BETACLASHCALLER" || message.text.ToUpper() == "!BETACC";
                               },
                               (message) =>
                               {
                                   if (StaticData.ClashCallerCode != "")
                                   {
                                       var getCCTask = Data.ClashCaller.GetClashCallerCode();
                                       getCCTask.RunSynchronously();
                                       var code = getCCTask.Result;
                                       bot.SendMessage("http://www.clashcaller.com/war/" + code);
                                   }
                                   else
                                   {
                                       bot.SendMessage("I don't have the code atm.");
                                   }
                               });
        }
    }
}
