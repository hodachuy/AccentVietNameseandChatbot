﻿using BotProject.Model.Models;
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
		public static void UpdateBotQnA(this BotQnAnswer botQnA, BotQnAnswerViewModel botQnAVm)
		{
			botQnA.ID = botQnAVm.ID;
			botQnA.Name = botQnAVm.Name;
			botQnA.Alias = botQnAVm.Alias;
			botQnA.BotID = botQnAVm.BotID;
			botQnA.Status = botQnAVm.Status;
		}

		public static void UpdateCard(this Card card, CardViewModel cardVm)
        {
            card.Name = cardVm.Name.ToUpper();
            card.BotID = cardVm.BotID;
            card.Alias = cardVm.Alias;
            card.TemplateJSON = cardVm.TemplateJSON;
        }

        public static void UpdateTemplateGenericGroup(this TemplateGenericGroup temGnrGroup, TemplateGenericGroupViewModel temGnrGroupVm)
        {
            temGnrGroup.ID = temGnrGroupVm.ID;
            temGnrGroup.Type = temGnrGroupVm.Type;
        }

        public static void UpdateTemplateGenericItem(this TemplateGenericItem temGnrItem, TemplateGenericItemViewModel temGnrItemVm)
        {
            temGnrItem.ID = temGnrItemVm.ID;
            temGnrItem.Image = temGnrItemVm.Image;
            temGnrItem.Title = temGnrItemVm.Title;
            temGnrItem.Url = temGnrItemVm.Url;
            temGnrItem.SubTitle = temGnrItemVm.Subtitle;
            temGnrItem.AttachmentID = temGnrItemVm.AttachmentID;
        }

        public static void UpdateButtonLink(this ButtonLink btnLink, ButtonLinkViewModel btnLinkVm)
        {
            btnLink.ID = btnLinkVm.ID;
            btnLink.Type = btnLinkVm.Type;
            btnLink.Title = btnLinkVm.Title;
            btnLink.Url = btnLinkVm.Url;
            btnLink.SizeHeight = btnLinkVm.SizeHeight;
        }
        public static void UpdateButtonPostback(this ButtonPostback btnPostback, ButtonPostbackViewModel btnPostbackVm)
        {
            btnPostback.ID = btnPostbackVm.ID;
            btnPostback.Type = btnPostbackVm.Type;
            btnPostback.Title = btnPostbackVm.Title;
            btnPostback.Payload = btnPostbackVm.Payload;
            btnPostback.CardPayloadID = btnPostbackVm.CardPayloadID;
        }

        public static void UpdateTemplateText(this TemplateText tempText, TemplateTextViewModel tempTextVm)
        {
            tempText.ID = tempTextVm.ID;
            tempText.Type = tempTextVm.Type;
            tempText.Text = tempTextVm.Text;
        }

        public static void UpdateQuickReply(this QuickReply quickReply, QuickReplyViewModel quickReplyVm)
        {
            quickReply.ID = quickReplyVm.ID;
            quickReply.CardPayloadID = quickReplyVm.CardPayloadID;
            quickReply.ContentType = quickReplyVm.ContentType;
            quickReply.Icon = quickReplyVm.Icon;
            quickReply.Title = quickReplyVm.Title;
        }
    }
}