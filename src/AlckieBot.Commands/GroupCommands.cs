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
        public static GroupCommands WhenInRomeChatCommands { get; set; }
        public static GroupCommands ReconChatCommands { get; set; }

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
                try
                {
                    command.Check(message);
                }
                catch (Exception ex)
                {
                    Bots.TestChatBot.SendMessage(ex.ToString());
                    //Idc, just keep looping through the commands if something goes wrong.
                }
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
                TestChatCommands.Commands.Add(GodCommands.GetGroupIDsCommand(Bots.TestChatBot));
            }

            if (Bots.LeadershipChatBot != null)
            {
                LeadershipChatCommands = new GroupCommands("leadership", Bots.LeadershipChatBot);
                LeadershipChatCommands.Commands.AddRange(GenericCommands.GetAllGenericCommands(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.AddRange(LeadershipCommands.GetAllLeadershipCommands(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.Add(WarCommands.ClashCallerCommand(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.Add(WarCommands.GetWarMatchUsCommand(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.Add(WarCommands.GetSetVillageCommand(Bots.LeadershipChatBot));
                LeadershipChatCommands.Commands.Add(WarCommands.GetGetVillageCommand(Bots.LeadershipChatBot));
            }

            if (Bots.WarChatBot != null)
            {
                WarChatCommands = new GroupCommands("war", Bots.WarChatBot);
                WarChatCommands.Commands.AddRange(WarCommands.GetAllWarCommands(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.CommandListCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.ShutupCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.BabyComeBackCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.EveryoneCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.CallModsCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.ModTagCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.TagMeInCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.TagMeInWithReasonCommand(Bots.WarChatBot));
                WarChatCommands.Commands.Add(GenericCommands.MemberJoinedCommand(Bots.WarChatBot));
            }

            if (Bots.GeneralChatBot != null)
            {
                GeneralChatCommands = new GroupCommands("general", Bots.GeneralChatBot);
                GeneralChatCommands.Commands.AddRange(GeneralCommands.GetAllGeneralCommands(Bots.GeneralChatBot));
                GeneralChatCommands.Commands.AddRange(GenericCommands.GetAllGenericCommands(Bots.GeneralChatBot));
                GeneralChatCommands.Commands.Add(GenericCommands.MemberJoinedCommand(Bots.GeneralChatBot));
            }

            if (Bots.WhenInRomeChatBot != null)
            {
                WhenInRomeChatCommands = new GroupCommands("wheninrome", Bots.WhenInRomeChatBot);

                WhenInRomeChatCommands.Commands.Add(GenericCommands.CommandListCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.UnixTimeCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.HiCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.BabyComeBackCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.ShutupCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.CuntCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.GifCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.RandomHandsUpCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.RollDiceCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.FlipACoinCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.TagMeInCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.TagMeInWithReasonCommand(Bots.WhenInRomeChatBot));
                WhenInRomeChatCommands.Commands.Add(GenericCommands.DontFlipTheTableCommand(Bots.WhenInRomeChatBot));
            }

            if (Bots.ReconChatBot != null)
            {
                ReconChatCommands = new GroupCommands("recon", Bots.ReconChatBot);

                ReconChatCommands.Commands.Add(ReconCommands.WelcomeMessageCommand(Bots.ReconChatBot));

                ReconChatCommands.Commands.Add(GenericCommands.CommandListCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.UnixTimeCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.HiCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.BabyComeBackCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.ShutupCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.CuntCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.GifCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.RandomHandsUpCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.RollDiceCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.FlipACoinCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.TagMeInCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.TagMeInWithReasonCommand(Bots.ReconChatBot));
                ReconChatCommands.Commands.Add(GenericCommands.DontFlipTheTableCommand(Bots.ReconChatBot));
            }
        }
    }
}
