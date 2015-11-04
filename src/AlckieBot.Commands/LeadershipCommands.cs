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
                GetSetCCCommand(bot)
            };
            return commands;
        }
        
        public static Command GetSetCCCommand(Bot bot)
        {
            return new Command("!setcc {CC code}","Set the clash caller code for the current war.","!setcc abc01", Command.CommandType.ModsOnly, bot,
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
