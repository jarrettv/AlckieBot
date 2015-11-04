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
                WelcomeMessageCommand(bot),
                RandomQuoteCommand(bot),
                SaveQuoteCommand(bot),
                RandomQuoteByMemberCommand(bot)
            };
            return commands;
        }
        
        public static Command WelcomeMessageCommand(Bot bot)
        {
            return new Command("", "", "", Command.CommandType.Automatic, bot, (message) =>
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

        public static Command SaveQuoteCommand(Bot bot)
        {
            return new Command("!savequote <tag> <quote>", "Save a quote for eternity.", "!savequote @Logan AlckieBot is amazing!", bot,
                               (message) =>
                               {
                                   var isSaveQuoteCommand = (message.text.ToUpper().StartsWith("!SAVEQUOTE "));
                                   if (!isSaveQuoteCommand)
                                   {
                                       //Return earlier
                                       return false;
                                   }
                                   var containsAttachment = message.attachments?.Length == 1;
                                   var isAMention = message.attachments[0]?.Type == "mentions";
                                   var containsOnlyOneMention = message.attachments[0]?.User_ids?.Length == 1;

                                   return isSaveQuoteCommand && containsAttachment && isAMention && containsOnlyOneMention;
                               },
                               (message) =>
                               {
                                   var mention = message.attachments[0];

                                   var quote = message.text.Substring(mention.Loci[0][0] + mention.Loci[0][1] + 1);
                                   var userName = message.text.Substring(mention.Loci[0][0] + 1, mention.Loci[0][1] - 1);
                                   var userID = mention.User_ids[0];


                                   if (mention.Loci[0][0] != "!SAVEQUOTE ".Length || quote.Length == 0)
                                   {
                                       var randomNumber = RandomHelper.GetRandomNumber(5);
                                       switch (randomNumber)
                                       {
                                           case 1:
                                               bot.SendMessage("Ask someone who knows his shit to do this for you.");
                                               break;
                                           case 2:
                                               bot.SendMessage("The command is all wrong!");
                                               break;
                                           case 3:
                                               bot.SendMessage("Try again, but try it right this time.");
                                               break;
                                           case 4:
                                               bot.SendMessage("\"!savequote <tag> <quote>\", ffs.");
                                               break;
                                           default:
                                               bot.SendMessage("That's not how it works.");
                                               break;
                                       }
                                   }
                                   else
                                   {
                                       Quote.AddQuote(new Model.Quote
                                       {
                                           Member = userName,
                                           SavedAt = DateTime.UtcNow,
                                           Message = quote
                                       });

                                       bot.SendMessage("This quote has been saved for eternity!");
                                   }
                               });
        }


        public static Command RandomQuoteCommand(Bot bot)
        {
            return new Command("!randomquote", "Return a random quote from someone", "", bot, (message) =>
            {
                return (message.text.ToUpper() == "!RANDOMQUOTE");
            },
            (message) =>
            {
                var quotes = Quote.GetQuotes();
                var randomNumber = RandomHelper.GetRandomNumber(quotes.Count);
                var selectedQuote = quotes[randomNumber - 1];

                bot.SendMessage($"\"{selectedQuote.Message.Trim()}\" - {selectedQuote.Member}, {selectedQuote.SavedAt.Year} ");
            });
        }

        public static Command RandomQuoteByMemberCommand(Bot bot)
        {
            return new Command("!randomquote <search parameters>", "Return a random quote from someone who matches the search parameters.", "!randomquote Alckie", bot, (message) =>
            {
                return message.text.ToUpper().StartsWith("!RANDOMQUOTE ");
            },
            (message) =>
            {
                var searchParams = message.text.Substring("!RANDOMQUOTE ".Length);
                var quotes = Quote.GetQuotes().Where(q => q.Member.ToUpper().Contains(searchParams.ToUpper())).ToList();
                if (quotes.Count > 0)
                {
                    var randomNumber = RandomHelper.GetRandomNumber(quotes.Count);
                    var selectedQuote = quotes[randomNumber - 1];

                    bot.SendMessage($"\"{selectedQuote.Message.Trim()}\" - {selectedQuote.Member}, {selectedQuote.SavedAt.Year} ");
                }
                else
                {
                    var randomNumber = RandomHelper.GetRandomNumber(3);
                    switch (randomNumber)
                    {
                        case 1:
                            bot.SendMessage("\"I can't find shit with that filter.\" - AlckieBot, 2015");
                            break;
                        case 2:
                            bot.SendMessage("Nothing to see here.");
                            break;
                        default:
                            bot.SendMessage("Search for something else.");
                            break;
                    }
                }

            });

        }
    }
}
