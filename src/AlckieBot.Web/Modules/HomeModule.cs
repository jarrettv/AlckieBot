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
using System;
using AlckieBot.Helpers;

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
                var commands = new List<Command>();
                string chatTitle = "";
                switch((string)parameters.group)
                {
                    case "war":
                        commands = GroupCommands.WarChatCommands.Commands;
                        chatTitle = "War";
                        break;
                    case "leadership":
                        commands = GroupCommands.LeadershipChatCommands.Commands;
                        chatTitle = "Leadership";
                        break;
                    case "general":
                        commands = GroupCommands.GeneralChatCommands.Commands;
                        chatTitle = "General";
                        break;
                    case "wheninrome":
                        commands = GroupCommands.WhenInRomeChatCommands.Commands;
                        chatTitle = "When In Rome";
                        break;
                    case "test":
                        commands = GroupCommands.TestChatCommands.Commands;
                        chatTitle = "Test";
                        break;
                }

                var publicCommands = commands.Where(c => c.Type == Command.CommandType.Public && !string.IsNullOrEmpty(c.Name)).OrderBy(c => c.Name).ToList();
                var modOnlyCommands = commands.Where(c => c.Type == Command.CommandType.ModsOnly && !string.IsNullOrEmpty(c.Name)).OrderBy(c => c.Name).ToList();

                var model = new
                {
                    ChatTitle = chatTitle,
                    PublicCommands = publicCommands,
                    ModOnlyCommands = modOnlyCommands
                };

                return View["commands.html", model];
            };
            Get["/Strikes"] = parameters =>
            {
                var strikes = Strikes.GetStrikes()
                .Where(s => s.StrickenAt.ToString("MM/yyyy") == DateTime.UtcNow.ToString("MM/yyyy"))
                .Select(s => new {
                    Member = s.Member.Name,
                    Reason = s.Reason,
                    StrickenBy = s.StrickenBy.Name,
                    StrickenAt = DateHelper.GetPrettyTimeSpan(DateTime.UtcNow.Subtract(s.StrickenAt))
                }).ToList();

                var model = new
                {
                    Month = DateTime.UtcNow.ToString("MMMM, yyyy"),
                    Strikes = strikes
                };

                return View["strikes.html", model];
            };
            Get["/Members"] = parameters =>
            {
                var members = Members.GetMembers()
                .Select(m => new
                {
                    Name = m.Name,
                    VillageCode = m.VillageCode,
                    IsBanned = m.Banned ? "Yes" : "No"
                }).ToList();

                return View["members.html", members];
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
                        case "wheninrome":
                            {
                                GroupCommands.WhenInRomeChatCommands.CheckMessage(message);
                                break;
                            }
                    }
                }

                return HttpStatusCode.OK;
            };
        }
    }
}
