using BotProject.Common.ViewModels;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BotProject.Web.API
{
    [RoutePrefix("api/history")]
    public class HistoryController : ApiControllerBase
    {
        private IHistoryService _historyService;

        public HistoryController(IHistoryService historyService, IErrorService errorService) : base(errorService)
        {
            _historyService = historyService;
        }

        [Route("getbybot")]
        [HttpGet]
        public HttpResponseMessage GetListQuestionAnswer(HttpRequestMessage request, int page, int pageSize, string botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                string filter = "BotID = " + botId;
                var lstHistory = _historyService.GetHistoryByBotId(filter, "", page, pageSize, null).ToList();
                if (lstHistory.Count() != 0)
                {
                    totalRow = lstHistory[0].Total;
                }
                var paginationSet = new PaginationSet<StoreProcHistoryViewModel>()
                {
                    Items = lstHistory,
                    Page = page,
                    TotalCount = totalRow,
                    MaxPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}
