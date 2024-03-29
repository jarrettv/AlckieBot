﻿using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class GodCommands
    {
        public static Command GetGroupIDsCommand(Bot bot)
        {
            return new Command("", "", "", Command.CommandType.GodOnly, bot,
                               (message) =>
                               {
                                   return message.text.ToUpper().StartsWith("!GROUPIDS");
                               },
                               (message) =>
                               {
                                   var groups = Chat.GetAllGroups(ConfigurationManager.AppSettings["GROUPME_TOKEN"]);
                                   var sb = new StringBuilder();
                                   foreach(var group in groups)
                                   {
                                       sb.AppendLine($"- Name: {group.Name} - ID: {group.ID}");
                                   }
                                   bot.SendMessage(sb.ToString());
                               });
        }

    }
}
