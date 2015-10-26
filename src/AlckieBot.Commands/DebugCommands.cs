using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class DebugCommands
    {
        public static List<Command> GetAllDebugCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                BeADoucheCommand(bot),
                StopBeingADoucheCommand(bot)
            };
            return commands;
        }

        private static Command BeADoucheCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.sender_id == Mods.Alckie && message.text.ToUpper() == "!BEADOUCHE";
                               },
                               (message) =>
                               {
                                   if (bot.IsBeingADouche)
                                   {
                                       bot.SendMessage("I'M DOING IT ALREADY, DAMNIT.");
                                   }
                                   else
                                   {
                                       bot.IsBeingADouche = true;
                                       bot.SendMessage("Douche mode on.");
                                   }
                               });
        }

        private static Command StopBeingADoucheCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return message.sender_id == Mods.Alckie && message.text.ToUpper() == "!STOPBEINGADOUCHE";
                               },
                               (message) =>
                               {
                                   if (bot.IsBeingADouche)
                                   {
                                       bot.IsBeingADouche = false;
                                       bot.SendMessage("Douche mode off.");
                                   }
                                   else
                                   {
                                       bot.SendMessage("But I'm not. ;_;");
                                   }
                               });
        }
    }
}
