using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public class GroupCommands
    {
        public static GroupCommands LeadershipChatCommands { get; set; }
        public static GroupCommands GeneralChatCommands { get; set; }
        public static GroupCommands WarChatCommands { get; set; }
        public static GroupCommands TestChatCommands { get; set; }
        public static GroupCommands AsylumChatCommands { get; set; }

        public string GroupName { get; set; }

        public Bot Bot { get; set; }

        public List<Command> Commands { get; set; }

        public GroupCommands(string groupName, Bot bot)
        {
            this.Bot = bot;
            this.GroupName = groupName;
            this.Commands = new List<Command>();
        }

        public void CheckMessage(ReceivedMessage message)
        {
            if (this.Bot.IsBeingADouche && Bots.TestChatBot != null)
            {
                Bots.TestChatBot.SendMessage(message.ToJsonString());
            }
            foreach (var command in this.Commands)
            {
                command.Check(message);
            }
        }

        public static void Init()
        {
            if (Bots.TestChatBot != null)
            {
                TestChatCommands = new GroupCommands("test", Bots.TestChatBot);
                TestChatCommands.Commands.AddRange(GenericCommands.GetAllGenericCommands(Bots.TestChatBot));
                TestChatCommands.Commands.AddRange(LeadershipCommands.GetAllLeadershipCommands(Bots.TestChatBot));
                TestChatCommands.Commands.AddRange(WarCommands.GetAllWarCommands(Bots.TestChatBot));
                TestChatCommands.Commands.Add(GodCommands.SendMessageToGeneralCommand(Bots.GeneralChatBot));
                TestChatCommands.Commands.Add(GodCommands.SendMessageToLeadershipCommand(Bots.LeadershipChatBot));
                TestChatCommands.Commands.Add(GodCommands.GetGroupIDsCommand(Bots.TestChatBot));
            }

            if (Bots.LeadershipChatBot != null)
            {
                LeadershipChatCommands = new GroupCommands("leadership", Bots.LeadershipChatBot);
                LeadershipChatCommands.Commands.AddRange(GenericCommands.GetAllGenericCommands(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.AddRange(LeadershipCommands.GetAllLeadershipCommands(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.Add(WarCommands.GetClashCallerCommand(Bots.LeadershipChatBot));
            }

            if (Bots.WarChatBot != null)
            {
                WarChatCommands = new GroupCommands("war", Bots.WarChatBot);
                WarChatCommands.Commands.AddRange(WarCommands.GetAllWarCommands(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetShutupCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetBabyComeBackCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetEveryoneCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetCallModsCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetModTagCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.GetTagMeInCommand(Bots.WarChatBot));
            }

            if (Bots.GeneralChatBot != null)
            {
                GeneralChatCommands = new GroupCommands("general", Bots.GeneralChatBot);
                GeneralChatCommands.Commands.AddRange(GeneralCommands.GetAllGeneralCommands(Bots.GeneralChatBot));
                GeneralChatCommands.Commands.AddRange(GenericCommands.GetAllGenericCommands(Bots.GeneralChatBot));
            }

            if (Bots.AsylumChatBot != null)
            {
                AsylumChatCommands = new GroupCommands("asylum", Bots.AsylumChatBot);
                AsylumChatCommands.Commands.Add(GenericCommands.GetBabyComeBackCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetShutupCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetRandomHandsUpCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetHiCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetGifCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetRollDiceCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetCuntCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetFlipACoinCommand(Bots.AsylumChatBot));
                AsylumChatCommands.Commands.Add(GenericCommands.GetTagMeInCommand(Bots.AsylumChatBot));
            }
        }
    }
}
