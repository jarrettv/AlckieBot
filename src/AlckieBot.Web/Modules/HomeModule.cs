using AlckieBot.Commands;
using AlckieBot.Data;
using AlckieBot.Web.Config;
using AlckieBot.Model.GroupMe;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AlckieBot.Web.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : base()
        {
            Get["/"] = _ => { return "Hello world!"; };
            Get["/Commands/{group}"] = parameters =>
            {
                var group = (string)parameters.group;

                var commandList = new StringBuilder();
                commandList.AppendLine("GENERIC COMMANDS:<br />");
                commandList.AppendLine("!commands - Will point you here.<br />");
                if (group != "war")
                {
                    commandList.AppendLine("!hi, !hey, !hello, !howdy - AlckieBot will greet you.<br />");
                    commandList.AppendLine("!cunt - AlckieBot will tell you if your message was received.<br />");
                    commandList.AppendLine("!id - Gets your GroupMe user ID.<br />");
                    commandList.AppendLine("!roll XdY - Rolls X dices of Y sides.<br />");
                    commandList.AppendLine("!gif <search terms> - AlckieBot will use his inferior partner, giphybot, to search for a gif.<br />");
                }
                commandList.AppendLine("!shutup - AlckieBot will stop speaking if you are one of his masters, otherwise, he will just be a cunt.<br />");
                commandList.AppendLine("!babycomeback - AlckieBot will go back to speaking, if you are his master.<br />");

                commandList.AppendLine("<br />GROUP SPECIFIC COMMANDS:<br />");
                switch (group)
                {
                    case "leadership":
                        {
                            commandList.AppendLine("!setcc - Sets the Clash Caller code.<br />");
                            commandList.AppendLine("!clashcaller, !cc - Gets the Clash Caller url.<br />");
                            break;
                        }
                    case "war":
                        {
                            commandList.AppendLine("!clashcaller, !cc - Gets the Clash Caller url.<br />");
                            break;
                        }
                }

                return commandList.ToString();
            };
            Post["/Incoming/{group}"] = parameters =>
            {
                var message = this.Bind<ReceivedMessage>();
                var group = (string)parameters.group;
                var botName = ConfigurationManager.AppSettings["BOT_NAME"];

                if (message.name != botName)
                {
                    switch (group)
                    {
                        case "general":
                            {
                                GroupCommands.GeneralChatCommands.CheckMessage(message);
                                break;
                            }
                        case "test":
                            {
                                GroupCommands.TestChatCommands.CheckMessage(message);
                                break;
                            }
                        case "leadership":
                            {
                                GroupCommands.LeadershipChatCommands.CheckMessage(message);
                                break;
                            }
                        case "war":
                            {
                                GroupCommands.WarChatCommands.CheckMessage(message);
                                break;
                            }
                        case "asylum":
                            {
                                GroupCommands.AsylumChatCommands.CheckMessage(message);
                                break;
                            }
                    }
                }

                return HttpStatusCode.OK;
            };
        }
    }
}
