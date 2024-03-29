﻿using AlckieBot.Data;
using AlckieBot.Helpers;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class GenericCommands
    {
        public static List<Command> GetAllGenericCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                CommandListCommand(bot),
                UnixTimeCommand(bot),
                HiCommand(bot),
                BabyComeBackCommand(bot),
                ShutupCommand(bot),
                CuntCommand(bot),
                GifCommand(bot),
                RandomHandsUpCommand(bot),
                RollDiceCommand(bot),
                FlipACoinCommand(bot),
                EveryoneCommand(bot),
                CallModsCommand(bot),
                ModTagCommand(bot),
                TagMeInCommand(bot),
                TagMeInWithReasonCommand(bot),
                KickCommand(bot),
                DontFlipTheTableCommand(bot)
            };
            commands.AddRange(DebugCommands.GetAllDebugCommands(bot));
            return commands;
        }

        public static Command CommandListCommand(Bot bot)
        {
            return new Command("!commands", "Get a url with all the available commands.", "", bot, (message) =>
            {
                return (message.text.ToUpper() == "!COMMANDS");
            },
            (message) =>
            {
                bot.SendMessage($"http://alckiebot.azurewebsites.net/commands/{bot.GroupName}");
            });
        }

        public static Command HiCommand(Bot bot)
        {
            return new Command("!hi, !hey, !howdy, !hello", "AlckieBot will greet you back.", "", bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper() == "!HI" ||
                                           message.text.ToUpper() == "!HEY" ||
                                           message.text.ToUpper() == "!HOWDY" ||
                                           message.text.ToUpper() == "!HELLO" ||
                                           message.text.ToUpper() == "@ALCKIEBOT");
                               },
                               (message) =>
                               {
                                   var messageNumber = RandomHelper.GetRandomNumber(5);
                                   switch (messageNumber)
                                   {
                                       case 1:
                                           bot.SendMessage("Hey, cunt!");
                                           break;
                                       case 2:
                                           bot.SendMessage("Hi");
                                           break;
                                       case 3:
                                           {
                                               var attachments = new List<dynamic>();
                                               attachments.Add(new
                                               {
                                                   loci = new int[][]
                                                   {
                                                        new int[]
                                                        {
                                                            4,
                                                            message.name.Length + 1
                                                        }
                                                   },
                                                   type = "mentions",
                                                   user_ids = new string[]
                                                    {
                                                        message.sender_id
                                                    }
                                               });
                                               bot.SendMessage("Sup @" + message.name + "!", attachments);
                                               break;
                                           }
                                       case 4:
                                           bot.SendMessage("Hello sir! Or lady. Or Siscim...");
                                           break;
                                       default:
                                           {
                                               var attachments = new List<dynamic>();
                                               attachments.Add(new
                                               {
                                                   loci = new int[][]
                                                   {
                                                        new int[]
                                                        {
                                                            3,
                                                            message.name.Length + 1
                                                        }
                                                   },
                                                   type = "mentions",
                                                   user_ids = new string[]
                                                    {
                                                        message.sender_id
                                                    }
                                               });
                                               bot.SendMessage("Hi @" + message.name + "!", attachments);
                                               break;
                                           }
                                   }
                               });
        }
        public static Command ShutupCommand(Bot bot)
        {
            return new Command("!shutup", "AlckieBot won't reply to commands anymore. Use !backcomebacy to restart him.", "", Command.CommandType.ModsOnly, bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper() == "!SHUTUP");
                               },
                               (message) =>
                               {
                                   if (Mods.IsUserAMod(message.sender_id))
                                   {
                                       bot.SendMessage("Sure thing, master. I will shut up for now.");
                                       bot.CanSpeak = false;
                                   }
                                   else
                                   {
                                       var attachments = new List<dynamic>();
                                       attachments.Add(new
                                       {
                                           loci = new int[][]
                                           {
                                            new int[]
                                            {
                                                17,
                                                message.name.Length + 1
                                            }
                                           },
                                           type = "mentions",
                                           user_ids = new string[]
                                           {
                                            message.sender_id
                                           }
                                       });
                                       bot.SendMessage("No, you shut up, @" + message.name + "!", attachments);
                                   }
                               });
        }

        public static Command BabyComeBackCommand(Bot bot)
        {
            return new Command("!babycomeback, !starttheparty", "AlckieBot will go back to his usual self.", "", Command.CommandType.ModsOnly, bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper() == "!BABYCOMEBACK" || message.text.ToUpper() == "!STARTTHEPARTY");
                               },
                               (message) =>
                               {
                                   if (Mods.IsUserAMod(message.sender_id))
                                   {
                                       var attachments = new List<dynamic>();
                                       attachments.Add(new
                                       {
                                           loci = new int[][]
                                            {
                                            new int[]
                                            {
                                                17,
                                                message.name.Length + 1
                                            }
                                            },
                                           type = "mentions",
                                           user_ids = new string[]
                                            {
                                            message.sender_id
                                            }
                                       });

                                       bot.SendMessage("Thank you Master @" + message.name + " for letting me be free!", attachments);
                                       bot.CanSpeak = true;
                                   }
                                   else
                                   {
                                       bot.SendMessage("No can do.");
                                   }
                               }, false);
        }

        public static Command CuntCommand(Bot bot)
        {
            return new Command("!cunt", "Send a cunt.", "", bot, (message) =>
            {
                return (message.text.ToUpper() == "!CUNT" || message.text.ToUpper() == "CUNT");
            },
            (message) =>
            {
                bot.SendMessage("Cunt received!");
            });
        }
        public static Command UnixTimeCommand(Bot bot)
        {
            return new Command("!time", "Gets the current date and time in Unix time.", "", bot, (message) =>
            {
                return (message.text.ToUpper() == "!TIME");
            },
            (message) =>
            {
                var unixTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                bot.SendMessage($"{unixTime} UTC.");
            });
        }
        public static Command FlipACoinCommand(Bot bot)
        {
            return new Command("!flipacoin", "AlckieBot will flip a coin for you.", "", bot, (message) =>
               {
                   return (message.text.ToUpper() == "!FLIPACOIN");
               },
            (message) =>
            {
                //Dice roll method is more precise with the odds than the random number method.
                var diceRoll = RandomHelper.RollDice(2);
                if (diceRoll == 1)
                {
                    bot.SendMessage("Head!");
                }
                else
                {
                    bot.SendMessage("Tail!");
                }
            });
        }

        public static Command RollDiceCommand(Bot bot)
        {
            return new Command("!roll {X}d{Y}", "Roll X dices with Y faces.", "!roll 1d6", bot, (message) =>
            {
                return (message.text.ToUpper().StartsWith("!ROLL "));
            },
            (message) =>
            {
                var diceParams = message.text.Substring(6);
                const string diceValidatorRegex = @"^(\d+)?d(\d+)$";
                var validate = Regex.Match(diceParams, diceValidatorRegex, RegexOptions.IgnoreCase);
                if (validate.Success)
                {
                    var diceArray = diceParams.ToUpper().Split('D');
                    var numberOfDices = Int32.Parse(diceArray[0]);
                    var diceSides = Int32.Parse(diceArray[1]);
                    if (numberOfDices == 0 || diceSides < 2)
                    {
                        bot.SendMessage("Are you dumb or something?");
                    }
                    else if (numberOfDices > 10)
                    {
                        bot.SendMessage("Nah. Too tired to roll that many dice...");
                    }
                    else if (diceSides > 100)
                    {
                        bot.SendMessage("That's not a dice. That's a fucking ball.");
                    }
                    else
                    {
                        var rollResults = new string[numberOfDices];
                        for (var i = 0; i < numberOfDices; i++)
                        {
                            var rollResult = RandomHelper.RollDice(diceSides);
                            rollResults[i] = rollResult.ToString();
                        }
                        bot.SendMessage($"The results for your roll are: {String.Join(",", rollResults)}");
                    }
                }
                else
                {
                    var randomNumber = RandomHelper.GetRandomNumber(5);
                    switch (randomNumber)
                    {
                        case 1:
                            bot.SendMessage("Gotta roll it right...");
                            break;
                        case 2:
                            bot.SendMessage("Nope.");
                            break;
                        case 3:
                            bot.SendMessage("Try again!");
                            break;
                        case 4:
                            bot.SendMessage("Omg, you can't even roll a dice.");
                            break;
                        default:
                            bot.SendMessage("That's not how it works.");
                            break;
                    }
                }
            });
        }

        public static Command RandomHandsUpCommand(Bot bot)
        {
            return new Command("", "", "", bot, (message) =>
            {
                return (message.text.Contains(@"\o/"));
            },
            (message) =>
            {
                var randomNumber = RandomHelper.GetRandomNumber(10);
                //Only 10% chance of this command being executed.
                if (randomNumber == 1)
                {
                    bot.SendMessage(@"\o/");
                }
            });
        }
        public static Command DontFlipTheTableCommand(Bot bot)
        {
            return new Command("", "", "", bot, (message) =>
            {
                return (new Regex(@"[(].+[)）].+┻[━]{0,}┻")).Match($"{message.text}|{message.name}").Success;
            },
            (message) =>
            {
                int flipTableCounter;
                bot.CustomCounters.TryGetValue("FlipTableCounter", out flipTableCounter);
                switch (flipTableCounter)
                {
                    case 0:
                        bot.SendMessage(@"┬─┬﻿ノ(°-°ノ) Please dont flip the table.");
                        break;
                    case 1:
                        bot.SendMessage(@"┬─┬﻿ノ(>_<ノ) Please dont flip the fucking table.");
                        break;
                    case 2:
                        bot.SendMessage(@"┬─┬﻿ノ(ò_óノ) STOP FLIPPING THE FUCKING TABLE!");
                        break;
                    case 3:
                        bot.SendMessage(@"(╯>_<）╯︵ /(.□.\)  FUCK YOU!.");
                        Thread.Sleep(100);
                        bot.SendMessage(@"┬─┬﻿ノ(°-°ノ)");
                        break;
                    case 4:
                        bot.SendMessage(@"(/.□.)\ ︵╰('□')╯︵ /(.□.\) Fuck you all, I'm done with this.");
                        break;
                }
                if (flipTableCounter < 4)
                {
                    flipTableCounter++;
                    bot.CustomCounters["FlipTableCounter"] = flipTableCounter;
                    TimerHelper.ExecuteDelayedActionAsync(() => 
                    {
                        bot.CustomCounters["FlipTableCounter"]--;
                    }, new TimeSpan(0, 10, 0));
                }
            });
        }

        public static Command TagMeInCommand(Bot bot)
        {
            return new Command("!tagmein {X}h{Y}m", "AlckieBot will tag you after some time.", "!tagmein 0h10m, !tagmein 10m", bot,
                               (message) =>
                               {
                                   var validate = Regex.Match(message.text.ToUpper(), @"^!TAGMEIN ((\d+)h)?((\d+)m)$", RegexOptions.IgnoreCase);
                                   return validate.Success;
                               },
                               (message) =>
                               {
                                   var parameters = message.text.Substring("!TAGMEIN ".Length);
                                   var hours = 0;
                                   var minutes = 0;

                                   if (parameters.Contains("h"))
                                   {
                                       hours = Int32.Parse(parameters.Split('h')[0]);
                                       minutes = Int32.Parse(parameters.Replace("m", "").Split('h')[1]);
                                   }
                                   else
                                   {
                                       minutes = Int32.Parse(parameters.Replace("m", ""));
                                   }
                                   var time = new TimeSpan(hours, minutes, 0);
                                   bot.SendMessage("Sure thing. I will tag you in " + parameters);
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
                                                        message.name.Length + 1
                                                    }
                                           },
                                           type = "mentions",
                                           user_ids = new string[]
                                           {
                                                    message.sender_id
                                           }
                                       });

                                       bot.SendMessage("@" + message.name, attachments);
                                   }, time);

                               });
        }

        public static Command TagMeInWithReasonCommand(Bot bot)
        {
            return new Command("!tagmein {X}h{Y}m {reminder}", "AlckieBot will tag you after some time, and remind you of something.", "!tagmein 0h10m Call my dad.", bot,
                               (message) =>
                               {
                                   var validate = Regex.Match(message.text.ToUpper(), @"^!TAGMEIN ((\d+)h)?((\d+)m) ", RegexOptions.IgnoreCase);
                                   return validate.Success;
                               },
                               (message) =>
                               {
                                   var validate = Regex.Match(message.text, @"^!TAGMEIN ((\d+)h)?((\d+)m) ", RegexOptions.IgnoreCase);

                                   var timeParameter = validate.Value.Substring("!TAGMEIN ".Length);
                                   var reasonParameter = message.text.Substring(validate.Value.Length);
                                   var hours = 0;
                                   var minutes = 0;

                                   if (timeParameter.Contains("h"))
                                   {
                                       hours = Int32.Parse(timeParameter.Split('h')[0]);
                                       minutes = Int32.Parse(timeParameter.Replace("m", "").Split('h')[1]);
                                   }
                                   else
                                   {
                                       minutes = Int32.Parse(timeParameter.Replace("m", ""));
                                   }
                                   var time = new TimeSpan(hours, minutes, 0);
                                   bot.SendMessage("Sure thing. I will tag you in " + timeParameter);
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
                                                        message.name.Length + 1
                                                    }
                                           },
                                           type = "mentions",
                                           user_ids = new string[]
                                           {
                                                    message.sender_id
                                           }
                                       });

                                       bot.SendMessage($"@{message.name}! You told me to remind you: {reasonParameter}.", attachments);
                                   }, time);
                               });
        }

        public static Command GifCommand(Bot bot)
        {
            return new Command("!gif {search parameters}", "Search for a random gif in Giphy.", "!gif angelina taxi srs", bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper().StartsWith("!GIF "));
                               },
                               (message) =>
                               {
                                   const int maxGifCounter = 3;
                                   var timeBeforeDecreasingGifCounter = new TimeSpan(0, 5, 0);
                                   var userGifCounter = bot.GetUserGifSpamCounter(message.sender_id);
                                   if (userGifCounter.GifCounter < maxGifCounter)
                                   {
                                       userGifCounter.HasBeenWarned = false;

                                       var searchParams = message.text.Substring(5);
                                       if (searchParams.ToUpper().Contains("ALCKIE") && searchParams.ToUpper().Contains("BOT"))
                                       {
                                           var randomNumber = RandomHelper.GetRandomNumber(5);
                                           switch (randomNumber)
                                           {
                                               case 1:
                                                   bot.SendMessage("Try searching for awesome instead.");
                                                   break;
                                               case 2:
                                                   bot.SendMessage("Do you know how many times people search for that? Fucking boring...");
                                                   break;
                                               case 3:
                                                   bot.SendMessage("I won't search for it, just to avoid AlckieBotception.");
                                                   break;
                                               case 4:
                                                   bot.SendMessage("There are no gifs in this world that can represent my awesome self.");
                                                   break;
                                               default:
                                                   bot.SendMessage("Fuck off.");
                                                   break;
                                           }
                                       }
                                       else
                                       {
                                           var url = Giphy.GetGiphyUrl(searchParams);
                                           if (url != "")
                                           {
                                               bot.SendMessage(url);
                                               userGifCounter.GifCounter++;
                                               TimerHelper.ExecuteDelayedActionAsync(() =>
                                               {
                                                   userGifCounter.GifCounter--;
                                               }, timeBeforeDecreasingGifCounter);
                                           }
                                           else
                                           {
                                               bot.SendMessage("I can't find shit. Either you are an idiot, or Giphy sucks too much...");
                                           }
                                       }
                                   }
                                   else if (!userGifCounter.HasBeenWarned)
                                   {
                                       userGifCounter.HasBeenWarned = true;
                                       bot.SendMessage("Fuck off... Just leave me alone for a while.");
                                   }
                               });
        }
        public static Command EveryoneCommand(Bot bot)
        {
            return new Command("@everyone", "Tag every member in the chat.", "", Command.CommandType.ModsOnly, bot,
                               (message) =>
                               {
                                   return Mods.IsUserAMod(message.sender_id) &&
                                                         (message.text.ToUpper() == "@EVERYONE");
                               },
                               (message) =>
                               {
                                   var group = Chat.GetGroup(ConfigurationManager.AppSettings["GROUPME_TOKEN"], bot.GroupID);
                                   if (group != null)
                                   {
                                       var callMessage = $"Hey everyone, listen up! {message.name} has something to tell you.";
                                       var userIDs = group.Members.Select(g => g.UserID).ToArray();
                                       var locis = new int[group.Members.Count][];
                                       for (var i = 0; i < group.Members.Count; i++)
                                       {
                                           if (i < group.Members.Count - 1)
                                           {
                                               locis[i] = new int[]
                                                {
                                                        i,
                                                        1
                                                };
                                           }
                                           else
                                           {
                                               //fill out the mention with the rest of the message
                                               locis[i] = new int[]
                                               {
                                                       i,
                                                       callMessage.Length - i
                                               };
                                           }
                                       }
                                       var attachments = new List<dynamic>();
                                       attachments.Add(new
                                       {
                                           loci = locis,
                                           type = "mentions",
                                           user_ids = userIDs
                                       });
                                       bot.SendMessage(callMessage, attachments);
                                   }
                               });
        }

        public static Command CallModsCommand(Bot bot)
        {
            return new Command("@mods", "Tag all Mist mods.", "", bot,
                               (message) =>
                               {
                                   return message.text.ToUpper() == "@MODS";
                               },
                               (message) =>
                               {
                                   if (bot.CanCallMods)
                                   {
                                       var leadershipGroup = Chat.GetGroup(ConfigurationManager.AppSettings["GROUPME_TOKEN"], ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"]);
                                       if (leadershipGroup != null)
                                       {
                                           var callMessage = "Masters, one of your minions needs your attention.";
                                           if (bot.GroupID == ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"])
                                           {
                                               callMessage = "Listen up, ya cunts!";
                                           }
                                           if (callMessage.Length < leadershipGroup.Members.Count)
                                           {
                                               var charactersToFill = leadershipGroup.Members.Count - callMessage.Length;
                                               callMessage = callMessage.PadRight(charactersToFill);
                                           }
                                           var userIDs = leadershipGroup.Members.Select(g => g.UserID).ToArray();
                                           var locis = new int[leadershipGroup.Members.Count][];
                                           for (var i = 0; i < leadershipGroup.Members.Count; i++)
                                           {
                                               if (i < leadershipGroup.Members.Count - 1)
                                               {
                                                   locis[i] = new int[]
                                                    {
                                                        i,
                                                        1
                                                    };
                                               }
                                               else
                                               {
                                                   //fill out the mention with the rest of the message
                                                   locis[i] = new int[]
                                                   {
                                                       i,
                                                       callMessage.Length - i
                                                   };
                                               }
                                           }

                                           var attachments = new List<dynamic>();
                                           attachments.Add(new
                                           {
                                               loci = locis,
                                               type = "mentions",
                                               user_ids = userIDs
                                           });
                                           bot.SendMessage(callMessage, attachments);
                                       }
                                   }
                                   else
                                   {
                                       bot.SendMessage("Plebs are not allowed to call the Masters right now.");
                                   }
                               });
        }

        public static Command ModTagCommand(Bot bot)
        {
            return new Command("!modtag", "Set whether plebs can tag the mods or not.", "", Command.CommandType.ModsOnly, bot,
                               (message) =>
                               {
                                   return (Mods.IsUserAMod(message.sender_id) && message.text.ToUpper() == "!MODTAG");
                               },
                               (message) =>
                               {
                                   bot.CanCallMods = !bot.CanCallMods;
                                   if (bot.CanCallMods)
                                   {
                                       bot.SendMessage("Plebs are now allowed to call the Masters");
                                   }
                                   else
                                   {
                                       bot.SendMessage("Plebs lost the right to call upon the Masters. Now stop messing around before we take away your other rights.");
                                   }
                               });
        }

        public static Command KickCommand(Bot bot)
        {
            return new Command("!kick {tag}", "Kick someone from the chat", "!kick @Cha", Command.CommandType.ModsOnly, bot,
                               (message) =>
                               {
                                   var isSaveQuoteCommand = (message.text.ToUpper().StartsWith("!KICK "));
                                   if (!isSaveQuoteCommand)
                                   {
                                       //Return earlier
                                       return false;
                                   }
                                   var isAMod = Mods.AllMods.Contains(message.user_id);
                                   var containsAttachment = message.attachments?.Length == 1;
                                   var isAMention = message.attachments[0]?.Type == "mentions";
                                   var containsOnlyOneMention = message.attachments[0]?.User_ids?.Length == 1;
                                   return isAMod && containsAttachment && isAMention && containsOnlyOneMention;
                               },
                               (message) =>
                               {
                                   var mention = message.attachments[0];
                                   var userID = mention.User_ids[0];
                                   if (Mods.AllMods.Contains(userID))
                                   {
                                       bot.SendMessage("I won't turn against my Master.");
                                   }
                                   else
                                   {
                                       if (bot.KickUser(ConfigurationManager.AppSettings["GROUPME_TOKEN"], userID))
                                       {
                                           bot.SendMessage("rekt");
                                       }
                                       else
                                       {
                                           bot.SendMessage("Shit! I can't...");
                                       }
                                   }
                               });
        }

        public static Command MemberJoinedCommand(Bot bot)
        {
            return new Command("", "", "", bot, (message) =>
            {
                return (message.system &&
                       (
                           message.text.ToUpper().Contains("JOINED") ||
                           message.text.ToUpper().Contains("ENTROU")
                       ));
            },
            (message) =>
            {
                Members.UpdateMembersByGroupAndKickIfBanned(ConfigurationManager.AppSettings["GROUPME_TOKEN"], bot.GroupID, bot);
            });
        }

    }
}
