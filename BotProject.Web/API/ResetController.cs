using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;

namespace BotProject.Web.API
{
    [RoutePrefix("api/reset")]
    public class ResetController : ApiControllerBase
    {
        private BotServiceMedical _botService;
        public ResetController(IErrorService errorService) : base(errorService)
        {
            _botService = BotServiceMedical.BotInstance;
            _botService = null;
            _botService = BotServiceMedical.BotInstance;
        }

            
        [Route("get")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                response = request.CreateResponse(HttpStatusCode.OK, "YES");
                return response;
            });
        }
    }
}
