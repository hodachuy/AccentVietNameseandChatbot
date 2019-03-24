using AutoMapper;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BotProject.Web.API
{
	[RoutePrefix("api/botqna")]
    public class BotQnAController : ApiControllerBase
    {
		private IQnAService _qnaService;
		public BotQnAController(IErrorService errorService, IQnAService qnaService) : base(errorService)
        {
			_qnaService = qnaService;

		}

		[Route("create")]
		[HttpPost]
		public HttpResponseMessage Create(HttpRequestMessage request, BotQnAnswerViewModel botQnAVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;
				BotQnAnswer botQnADb = new BotQnAnswer();
				botQnADb.UpdateBotQnA(botQnAVm);
				var botQnAReturn = _qnaService.AddBotQnAnswer(ref botQnADb);
				try
				{
					// create file bot aiml
					string pathFolderAIML = ConfigurationManager.AppSettings["AIMLPath"] + "\\" + "User_" + botQnAVm.UserID + "_BotID_" + botQnAVm.BotID;
					string nameFolderAIML = "botQnA_ID_"+ botQnADb.ID + "_" + botQnADb.Alias + ".aiml";
					string pathString = System.IO.Path.Combine(pathFolderAIML, nameFolderAIML);
					if (!System.IO.File.Exists(pathString))
					{
						using (System.IO.FileStream fs = System.IO.File.Create(pathString))
						{
						}
					}
				}
				catch (Exception ex)
				{

				}
				var reponseData = Mapper.Map<BotQnAnswer, BotQnAnswerViewModel>(botQnAReturn);

				response = request.CreateResponse(HttpStatusCode.OK, reponseData);

				return response;
			});
		}
	}
}
