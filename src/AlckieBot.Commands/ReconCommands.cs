using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public class ReconCommands
    {
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
                bot.SendMessage("Welcome! As you can see, if you look at everyone's nickname in GM they have their in-game name (IGN) and their \"status\" so to speak, in parenthesis. This helps us keep track of who we can expect to see warring and who we can expect to not really see as active until the date they have in their status (due to real life stuff or whatever). Would you mind changing your nickname to reflect that as well?\n\nTo change it in GM, just press on the group icon on the upper right hand corner, go to settings, edit group and then edit your nickname there. Thanks!");
            });
        }
    }
}
