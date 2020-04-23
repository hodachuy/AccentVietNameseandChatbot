using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BotProject.Web.API_SV_172
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiControllerBase
    {
        private ApiQnaNLRService _apiNLR;
        public CommonController(IErrorService errorService) : base(errorService)
        {
            _apiNLR = new ApiQnaNLRService();
        }
        [Route("getRelatedArticles")]
        [HttpGet]
        public HttpResponseMessage GetRelatedArticles(HttpRequestMessage request,string content)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstArticle = _apiNLR.GetObjectRelatedArticle(content,"5");
                response = request.CreateResponse(HttpStatusCode.OK, lstArticle);
                return response;
            });
        }
    }
}
