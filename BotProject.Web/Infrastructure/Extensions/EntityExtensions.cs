using BotProject.Model.Models;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BotProject.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateBot(this Bot bot, BotViewModel botVm)
        {
            bot.ID = botVm.ID;
            bot.Name = botVm.Name;
            bot.Alias = botVm.Alias;
            bot.CreatedBy = botVm.CreatedBy;
            bot.CreatedDate = DateTime.Now;
            bot.UserID = botVm.UserID;
            bot.Status = botVm.Status;
        }
    }
}