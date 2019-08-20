using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using AutoMapper;
using BotProject.Model.Models;
using BotProject.Web.Models;
using BotProject.Web.Infrastructure.Extensions;
using System.Configuration;
using BotProject.Common;
using Newtonsoft.Json.Linq;

namespace BotProject.Web.API
{
	[RoutePrefix("api/bot")] 
	public class BotController : ApiControllerBase
	{
		private IBotService _botService;
        private ICardService _cardService;
        private ISettingService _settingService;
        private ICommonCardService _commonCardService;
        private IQnAService _qnaService;

        public BotController(IErrorService errorService,
            IBotService botService,
            ICardService cardService,
            ISettingService settingService,
            ICommonCardService commonCardService,
            IQnAService qnaService) : base(errorService)
		{
			_botService = botService;
            _settingService = settingService;
            _cardService = cardService;
            _commonCardService = commonCardService;
            _qnaService = qnaService;

        }	

		[Route("getall")]
		[HttpGet]
		public HttpResponseMessage GetBotByUserID(HttpRequestMessage request, string userID)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response;
				if (String.IsNullOrEmpty(userID))
				{
					response = request.CreateResponse(HttpStatusCode.NotFound);
					return response;
				}

				var lstBot = _botService.GetListBotByUserID(userID);

				var lstBotVm = Mapper.Map<IEnumerable<Bot>, IEnumerable<BotViewModel>>(lstBot);

				response = request.CreateResponse(HttpStatusCode.OK, lstBotVm);

				return response;
			});
		}

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, BotViewModel botVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (String.IsNullOrEmpty(botVm.UserID))
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound);
                    return response;
                }
                Bot botDb = new Bot();
                botDb.UpdateBot(botVm);
                var botReturn = _botService.Create(ref botDb);
				try
				{
					// create file bot aiml
					//string pathFolderAIML = ConfigurationManager.AppSettings["AIMLPath"];
                    string pathFolderAIML = PathServer.PathAIML;
                    string nameFolderAIML = "User_" +botVm.UserID + "_BotID_" + botReturn.ID;
					string pathString = System.IO.Path.Combine(pathFolderAIML, nameFolderAIML);
					System.IO.Directory.CreateDirectory(pathString);
				}
				catch (Exception ex)
				{

				}
                Setting settingDb = new Setting();
                settingDb.BotID = botDb.ID;
                settingDb.Color = "rgb(75, 90, 148);";
                settingDb.UserID = botVm.UserID;
                settingDb.Logo = "assets/images/user_bot.jpg";
                _settingService.Create(settingDb);
                _settingService.Save();
                var reponseData = Mapper.Map<Bot, BotViewModel>(botReturn);
                response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("deletebot")]
        [HttpPost]
        public HttpResponseMessage DeleteBot(HttpRequestMessage request, JObject jsonData)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                dynamic json = jsonData;
                int botID = json.botId;

                var botDb = _botService.GetByID(botID);
                botDb.Status = false;
                _botService.Update(botDb);
                _botService.Save();

                //delete database
                //var lstCard = _cardService.GetListCardByBotID(botID).ToList();
                //if (lstCard.Count() != 0)
                //{
                //    foreach (var item in lstCard)
                //    {
                //        _commonCardService.DeleteCard(item.ID);
                //    }
                //}

                //var lstBotQnAnswer = _qnaService.GetListBotQnAnswerByBotID(botID);
                //if (lstBotQnAnswer != null && lstBotQnAnswer.Count() != 0)
                //{
                //    foreach (var botQnAnswer in lstBotQnAnswer)
                //    {
                //        var lstQuesGroup = _qnaService.GetListQuestionGroupByBotQnAnswerID(botQnAnswer.ID);
                //        if (lstQuesGroup != null && lstQuesGroup.Count() != 0)
                //        {
                //            foreach (var quesGroup in lstQuesGroup)
                //            {
                //                _qnaService.DeleteQuesByQuestionGroup(quesGroup.ID);
                //                _qnaService.DeleteAnswerByQuestionGroup(quesGroup.ID);

                //            }
                //        }
                //    }
                //}
                response = request.CreateResponse(HttpStatusCode.OK, true);
                return response;
            });
        }
    }
}
