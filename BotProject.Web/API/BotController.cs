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

namespace BotProject.Web.API
{
	[RoutePrefix("api/bot")] 
	public class BotController : ApiControllerBase
	{
		private IBotService _botService;
		private ApplicationUserManager _userManager;
		public BotController(IErrorService errorService, IBotService botService) : base(errorService)
		{
			_botService = botService;
		}

		[Route("create")]
		[HttpPost]
		public HttpResponseMessage Create(HttpRequestMessage request, string userID, string botName)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response;
				if (String.IsNullOrEmpty(userID))
				{
					response = request.CreateErrorResponse(HttpStatusCode.RequestTimeout, "SessionTimeout");
					return response;
				}
				if(String.IsNullOrEmpty(botName))
				{
					response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ten trong");
					return response;
				}

				//Bot bot = new Bot();
				//bot.Name = botName;
				//bot.a

				var lstBot = _botService.GetListBotByUserID(userID);

				var lstBotVm = Mapper.Map<IEnumerable<Bot>, IEnumerable<BotViewModel>>(lstBot);

				response = request.CreateResponse(HttpStatusCode.OK, lstBotVm);

				return response;
			});
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

	}
}
