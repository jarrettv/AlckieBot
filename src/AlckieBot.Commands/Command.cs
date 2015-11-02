﻿using AlckieBot.Data;
using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlckieBot.Commands
{
    public class Command
    {
        public Bot Bot { get; set; }
        public bool CanBeMuted { get; set; }
        public Func<ReceivedMessage, bool> Condition { get; set; }
        public Action<ReceivedMessage> Response { get; set; }

        public Command(Bot bot, Func<ReceivedMessage, bool> condition, Action<ReceivedMessage> response, bool canBeMuted = true)
        {
            this.Bot = bot;
            this.Condition = condition;
            this.Response = response;
            this.CanBeMuted = canBeMuted;
        }

        public void Check(ReceivedMessage message)
        {
            if (this.Bot != null)
            {
                if (!(this.CanBeMuted && !this.Bot.CanSpeak) && Condition.Invoke(message))
                {
                    try
                    {
                        Thread.Sleep(150);
                        Response.Invoke(message);
                    }
                    catch (Exception)
                    {
                        this.Bot.SendMessage("Fuck! Something went wrong, tell Master Alckie to fix this shit.");
                    }
                }
            }
        }
    }
}
