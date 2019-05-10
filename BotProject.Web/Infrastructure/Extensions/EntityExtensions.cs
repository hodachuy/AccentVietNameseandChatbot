using BotProject.Model.Models;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using BotProject.Common;
using System.Text.RegularExpressions;

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
		public static void UpdateFormQnA(this FormQuestionAnswer formQnA, FormQuestionAnswerViewModel formQnAVm)
		{
            formQnA.ID = formQnAVm.ID;
            formQnA.Name = formQnAVm.Name;
            formQnA.Alias = formQnAVm.Alias;
            formQnA.BotID = formQnAVm.BotID;
            formQnA.Status = formQnAVm.Status;
		}

        #region Question-Answer-Group
        public static void UpdateQuestionGroup(this QuestionGroup qGroup, QuestionGroupViewModel qGroupVm)
        {
            qGroup.ID = qGroupVm.ID;
            qGroup.Index = qGroupVm.Index;
            qGroup.IsKeyword = qGroupVm.IsKeyWord;
            qGroup.FormQuestionAnswerID = qGroupVm.FormQuestionAnswerID;
            qGroup.BotID = qGroupVm.BotID;
			qGroup.CreatedDate = DateTime.Now;
        }

        public static void UpdateQuestion(this Question ques, QuestionViewModel quesVm)
        {
            ques.ID = quesVm.ID;
            ques.Index = quesVm.Index;
			ques.QuestionGroupID = quesVm.QuestionGroupID;
            ques.IsThatStar = false;
            ques.ContentText = quesVm.ContentText.Trim();
        }

        public static void UpdateQuestionIsStar(this Question ques, QuestionViewModel quesVm)
        {
            ques.ID = quesVm.ID;
            ques.Index = quesVm.Index;
			ques.QuestionGroupID = quesVm.QuestionGroupID;
			ques.IsThatStar = quesVm.IsThatStar;
            ques.ContentText = quesVm.ContentText.Trim() + " *";
        }

        public static void UpdateAnswer(this Answer answer, AnswerViewModel answerVm)
        {
            answer.ID = answerVm.ID;
            answer.Index = answerVm.Index;
            answer.CardPayload = answerVm.CardPayload;
			answer.QuestionGroupID = answerVm.QuestionGroupID;
			answer.CardID = answerVm.CardID;
            answer.ContentText = String.IsNullOrEmpty(answerVm.ContentText) == true ? "" : HttpUtility.HtmlDecode(answerVm.ContentText.Trim());
        }
        #endregion

        #region CARD
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

        public static void UpdateQuickReply(this BotProject.Model.Models.QuickReply quickReply, QuickReplyViewModel quickReplyVm)
        {
            quickReply.ID = quickReplyVm.ID;
            quickReply.CardPayloadID = quickReplyVm.CardPayloadID;
            if (quickReply.CardPayloadID != null)
            {
                quickReply.Payload = CommonConstants.PostBackCard + quickReply.CardPayloadID;
            }else
            {
                quickReply.Payload = "";
            }
            quickReply.ContentType = quickReplyVm.ContentType;
            quickReply.Icon = quickReplyVm.Icon;
            quickReply.Title = quickReplyVm.Title;
        }
        #endregion

        #region Setting
        public static void UpdateSetting(this Setting settingDb, BotSettingViewModel settingVm)
        {
            settingDb.ID = settingVm.ID;
            settingDb.BotID = settingVm.BotID;
            settingDb.CardID = settingVm.CardID;
            settingDb.Color = settingVm.Color;
            settingDb.FormName = settingVm.FormName;
            settingDb.IsMDSearch = settingVm.IsMDSearch;
            settingDb.Logo = settingVm.Logo;
            settingDb.TextIntroductory = settingVm.TextIntroductory;
            settingDb.UserID = settingVm.UserID;
            if (settingDb.CardID == null && String.IsNullOrEmpty(settingDb.TextIntroductory))
            {
                settingDb.IsActiveIntroductory = false;
            }
            else if (settingDb.CardID != null || !String.IsNullOrEmpty(settingDb.TextIntroductory))
            {
                settingDb.IsActiveIntroductory = true;
            }
        }
        #endregion

        #region Module QnA
        public static void UpdateModuleQuestion(this MdQuestion mdQuesDb, ModuleQnAViewModel mdQnA)
        {
            mdQuesDb.ID = mdQnA.QuesID.GetValueOrDefault();
            mdQuesDb.ContentHTML = HttpUtility.HtmlDecode(mdQnA.QuesContent);
            mdQuesDb.ContentText = Regex.Replace(HttpUtility.HtmlDecode(mdQnA.QuesContent), @"<(.|\n)*?>", "");
            mdQuesDb.AreaID = mdQnA.AreaID;
            mdQuesDb.CreatedDate = DateTime.Now;
        }
        public static void UpdateModuleAnswer(this MdAnswer mdAnsDb, ModuleQnAViewModel mdQnA)
        {
            mdAnsDb.ID = mdQnA.AnsID.GetValueOrDefault();
            mdAnsDb.ContentHTML = HttpUtility.HtmlDecode(mdQnA.AnsContent);
            mdAnsDb.ContentText = Regex.Replace(HttpUtility.HtmlDecode(mdQnA.AnsContent), @"<(.|\n)*?>", "");
            mdAnsDb.MQuestionID = mdQnA.QuesID;
        }
        #endregion
    }
}