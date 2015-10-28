using AlckieBot.Data;
using AlckieBot.Helpers;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public static class GenericCommands
    {
        public static List<Command> GetAllGenericCommands(Bot bot)
        {
            var commands = new List<Command>
            {
                GetHiCommand(bot),
                GetBabyComeBackCommand(bot),
                GetShutupCommand(bot),
                GetCuntCommand(bot),
                GetIDCommand(bot),
                GetGifCommand(bot),
                GetRandomHandsUpCommand(bot),
                GetRollDiceCommand(bot),
                GetFlipACoinCommand(bot),
                GetEveryoneCommand(bot),
                GetCallModsCommand(bot),
                GetModTagCommand(bot),
                GetTagMeInCommand(bot)
            };
            commands.AddRange(DebugCommands.GetAllDebugCommands(bot));
            return commands;
        }

        public static Command GetHiCommand(Bot bot)
        {
            return new Command(bot,
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
        public static Command GetShutupCommand(Bot bot)
        {
            return new Command(bot,
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

        public static Command GetBabyComeBackCommand(Bot bot)
        {
            return new Command(bot,
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

        public static Command GetCuntCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.ToUpper() == "!CUNT" || message.text.ToUpper() == "CUNT");
            },
            (message) =>
            {
                bot.SendMessage("Cunt received!");
            });
        }
        public static Command GetFlipACoinCommand(Bot bot)
        {
            return new Command(bot, (message) =>
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

        public static Command GetRollDiceCommand(Bot bot)
        {
            return new Command(bot, (message) =>
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

        public static Command GetRandomHandsUpCommand(Bot bot)
        {
            return new Command(bot, (message) =>
            {
                return (message.text.Contains(@"\o/"));
            },
            (message) =>
            {
                var randomNumber = RandomHelper.GetRandomNumber(10);
                //Only 5% chance of this command being executed.
                if (randomNumber == 1)
                {
                    bot.SendMessage(@"\o/");
                }
            });
        }

        public static Command GetIDCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper() == "!ID");
                               },
                               (message) =>
                               {
                                   bot.SendMessage("Your GroupMe ID is " + message.sender_id);
                               });
        }
        public static Command GetTagMeInCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper().StartsWith("!TAGMEIN "));
                               },
                               (message) =>
                               {
                                   var parameters = message.text.Substring("!TAGMEIN ".Length);
                                   const string regex = @"^((\d+)h)?(\d+)m$";
                                   var validate = Regex.Match(parameters, regex, RegexOptions.IgnoreCase);
                                   if (validate.Success)
                                   {
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
                                   }
                                   else
                                   {
                                       bot.SendMessage("Please specify the time in the following format: 1h0m");
                                   }

                               });
        }

        public static Command GetGifCommand(Bot bot)
        {
            return new Command(bot,
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
        public static Command GetEveryoneCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return Mods.IsUserAMod(message.sender_id) &&
                                                         (message.text.ToUpper() == "@EVERYONE" ||
                                                         message.text.ToUpper().StartsWith("@EVERYONE "));
                               },
                               (message) =>
                               {
                                   var groups = Chat.GetAllGroups(ConfigurationManager.AppSettings["GROUPME_TOKEN"]);
                                   var group = groups.FirstOrDefault(g => g.ID == bot.GroupID);
                                   if (group != null)
                                   {
                                       var callMessage = "@Everyone";
                                       var userIDs = group.Members.Select(g => g.UserID).ToArray();
                                       var locis = new int[group.Members.Count][];
                                       for (var i = 0; i < group.Members.Count; i++)
                                       {
                                           locis[i] = new int[]
                                            {
                                                0,
                                                callMessage.Length
                                            };
                                       }
                                       var attachments = new List<dynamic>();
                                       attachments.Add(new
                                       {
                                           loci = locis,
                                           type = "mentions",
                                           user_ids = userIDs
                                       });
                                       if (message.text.Length > "!EVERYONE ".Length)
                                       {
                                           callMessage += " " + message.text.Substring("!EVERYONE ".Length);
                                       }
                                       bot.SendMessage(callMessage, attachments);
                                   }
                               });
        }

        public static Command GetCallModsCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return Mods.IsUserAMod(message.sender_id) &&
                                          message.text.ToUpper() == "@MODS" &&
                                          bot.CanCallMods;
                               },
                               (message) =>
                               {
                                   var groups = Chat.GetAllGroups(ConfigurationManager.AppSettings["GROUPME_TOKEN"]);
                                   var leadershipGroup = groups.FirstOrDefault(g => g.ID == ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"]);
                                   if (leadershipGroup != null)
                                   {
                                       //var callMessage = "Masters, one of your minions need help.";
                                       //if (bot.GroupID == ConfigurationManager.AppSettings["LEADERSHIPCHAT_ID"])
                                       //{
                                       //    callMessage = "Listen up, ya cunts!";
                                       //}
                                       //var userIDs = leadershipGroup.Members.Select(g => g.UserID).ToArray();
                                       //var locis = new int[leadershipGroup.Members.Count][];
                                       //for (var i = 0; i < leadershipGroup.Members.Count; i++)
                                       //{
                                       //    locis[i] = new int[]
                                       //     {
                                       //         0,
                                       //         callMessage.Length
                                       //     };
                                       //}
                                       var userIDs = new string[leadershipGroup.Members.Count];
                                       var locis = new int[leadershipGroup.Members.Count][];
                                       var callMessage = "";
                                       var builder = new StringBuilder();
                                       builder.Append(callMessage);
                                       for (var i = 0; i < leadershipGroup.Members.Count; i++)
                                       {
                                           locis[i] = new int[]
                                           {
                                               builder.ToString().Length,
                                               leadershipGroup.Members[i].Name.Length + 1
                                           };
                                           userIDs[i] = leadershipGroup.Members[i].UserID;
                                           builder.Append("@" + leadershipGroup.Members[i].Name + " ");
                                       }
                                       callMessage = builder.ToString();


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

        public static Command GetModTagCommand(Bot bot)
        {
            return new Command(bot,
                               (message) =>
                               {
                                   return (message.text.ToUpper() == "!MODTAG");
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

    }
}
