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

namespace BotProject.Web.API
{
	[RoutePrefix("api/bot")] 
	public class BotController : ApiControllerBase
	{
		private IBotService _botService;
        private ISettingService _settingService;
		public BotController(IErrorService errorService,
            IBotService botService,
            ISettingService settingService) : base(errorService)
		{
			_botService = botService;
            _settingService = settingService;

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
					response = request.CreateErrorResponse(HttpStatusCode.RequestTimeout, "SessionTimeout");
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
                    response = request.CreateErrorResponse(HttpStatusCode.RequestTimeout, "SessionTimeout");
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
                settingDb.Color = "rgb(234, 82, 105);";
                settingDb.UserID = botVm.UserID;
                settingDb.Logo = "assets/images/user_bot.jpg";
                _settingService.Create(settingDb);
                _settingService.Save();
                var reponseData = Mapper.Map<Bot, BotViewModel>(botReturn);
                response = request.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }
    }
}
