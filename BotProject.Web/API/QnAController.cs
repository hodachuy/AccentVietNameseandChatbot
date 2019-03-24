using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Web.Models;

namespace BotProject.Web.API
{
    [RoutePrefix("api/qna")]
    public class QnAController : ApiControllerBase
    {
		private IQnAService _qnaService;
		public QnAController(IErrorService errorService, IQnAService qnaService) : base(errorService)
        {
			_qnaService = qnaService;

		}

		[Route("create")]
		[HttpPost]
		public HttpResponseMessage Create(HttpRequestMessage request, QnAnswerGroupViewModel qGroupVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;
				try
				{

				}
				catch(Exception ex)
				{

				}
				
				return response;
			});
		}
	}
}
