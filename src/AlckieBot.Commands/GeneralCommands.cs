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
    public static class GeneralCommands
    {
        public static List<Command> GetAllGeneralCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                GetCommandsCommand(bot),
                GetMemberJoinedCommand(bot),
                GetRandomLoganCommand(bot),
                GetPrettyBoyCommand(bot)
            };
            commands.AddRange(DebugCommands.GetAllDebugCommands(bot));
            return commands;
        }

        private static Command GetRandomLoganCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.sender_id == Pleb.Logan.ID && !message.text.ToUpper().StartsWith("!"));
            },
            (message) =>
            {
                var randomNumber = RandomHelper.GetRandomNumber(20);
                //Only 5% chance of this command being executed.
                if (randomNumber == 1)
                {
                    var randomMinutes = RandomHelper.GetRandomNumber(3);
                    var randomSeconds = RandomHelper.GetRandomNumber(30);
                    TimerHelper.ExecuteDelayedActionAsync(() =>
                    {
                        var attachments = new List<dynamic>();
                        attachments.Add(new
                        {
                            loci = new int[][]
                            {
                            new int[]
                            {
                                0,
                                Pleb.Logan.Name.Length + 1
                            }
                            },
                            type = "mentions",
                            user_ids = new string[]
                             {
                            Pleb.Logan.ID
                             }
                        });
                        bot.SendMessage("@" + Pleb.Logan.Name + "", attachments);
                    }, new TimeSpan(0, randomMinutes, randomSeconds));
                }
            });
        }

        public static Command GetPrettyBoyCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.ToUpper().Contains("@PRETTYBOY"));
            },
            (message) =>
            {
                var attachments = new List<dynamic>();
                attachments.Add(new
                {
                    loci = new int[][]
                    {
                            new int[]
                            {
                                message.text.ToUpper().IndexOf("@PRETTYBOY"),
                                "@PRETTYBOY".Length
                            }
                    },
                    type = "mentions",
                    user_ids = new string[]
                     {
                            Mods.Siscim
                     }
                });
                bot.SendMessage(message.text, attachments);
            });
        }

        public static Command GetCommandsCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.ToUpper() == "!COMMANDS");
            },
            (message) =>
            {
                bot.SendMessage("http://alckiebot.azurewebsites.net/commands/general");
            });
        }

        public static Command GetMemberJoinedCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.system &&
                       (
                           message.text.ToUpper().Contains("JOINED") ||
                           message.text.ToUpper().Contains("ENTROU") ||
                           message.text.ToUpper().Contains("ADICIONOU") ||
                           message.text.ToUpper().Contains("ADDED")
                       ));
            },
            (message) =>
            {
                bot.SendMessage("Welcome to Reddit Mist! Please change your name to match your IGN and, if you haven't already, make sure to read our rules available at www.reddit.com/r/RedditMist");
            });
        }
    }
}
