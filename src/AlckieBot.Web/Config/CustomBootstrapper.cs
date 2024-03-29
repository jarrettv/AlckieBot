﻿using Nancy.Bootstrappers.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Nancy.Bootstrapper;
using System.Configuration;
using AlckieBot.Model.GroupMe;
using Nancy.Diagnostics;
using Nancy;
using AlckieBot.Data;
using AlckieBot.Commands;

namespace AlckieBot.Web.Config
{
    public class CustomBootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            ClashCaller.Init();
            Bots.Init();
            GroupCommands.Init();
            Mods.Init();
            try
            {
                //The bot can live if the members consistency isn't verified.
                Members.UpdateMembersByGroup(ConfigurationManager.AppSettings["GROUPME_TOKEN"], ConfigurationManager.AppSettings["GENERALCHAT_ID"]);
                Members.UpdateMembersByGroup(ConfigurationManager.AppSettings["GROUPME_TOKEN"], ConfigurationManager.AppSettings["WARCHAT_ID"]);
            }
            catch (Exception ex)
            {
                Bots.TestChatBot.SendMessage("Error while saving members into the database");
                Bots.TestChatBot.SendMessage(ex.ToString());
            }

            base.ApplicationStartup(container, pipelines);
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
                return new DiagnosticsConfiguration { Password = ConfigurationManager.AppSettings["NANCY_DIAGNOSTICS_PASSWORD"] };
            }
        }
    }
}
