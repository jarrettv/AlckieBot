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
                GetClashCallerCommand(bot)
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
    }
}
