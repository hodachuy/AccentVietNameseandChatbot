using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Common.ViewModels;
using BotProject.Web.Models;
using BotProject.Model.Models;
using System.Text.RegularExpressions;
using System.Web;

namespace BotProject.Web.API
{
    [RoutePrefix("api/module")]
    public class ModuleController : ApiControllerBase
    {
        private IMdQnAService _searchService;
        public ModuleController(IErrorService errorService, IMdQnAService searchService) : base(errorService)
        {
            _searchService = searchService;
        }

        [Route("getallqna")]
        [HttpGet]
        public HttpResponseMessage GetByBotQnAnswerId(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var lstSearchQna = _searchService.GetListMdQnA("", "", page, pageSize,null).ToList();
                if (lstSearchQna.Count() != 0)
                {
                    totalRow = lstSearchQna[0].Total;
                }
                var paginationSet = new PaginationSet<MdQnAViewModel>()
                {
                    Items = lstSearchQna,
                    Page = page,
                    TotalCount = totalRow,
                    MaxPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("createupdateqna")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateQnA(HttpRequestMessage request,ModuleQnAViewModel mdQnA )
        {
            return CreateHttpResponse(request,() => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                    return response;
                }
                if(mdQnA.QuesID == null)
                {
                    MdQuestion mdQuesDb = new MdQuestion();
                    mdQuesDb.ContentHTML = HttpUtility.HtmlDecode(mdQnA.QuesContent);
                    mdQuesDb.ContentText = Regex.Replace(HttpUtility.HtmlDecode(mdQnA.QuesContent), @"<(.|\n)*?>", "");
                    mdQuesDb.AreaID = mdQnA.AreaID;
                    mdQuesDb.CreatedDate = DateTime.Now;
                    _searchService.CreateQuestion(mdQuesDb);
                    _searchService.Save();

                    MdAnswer mdAnsDb = new MdAnswer();
                    mdAnsDb.ContentHTML = HttpUtility.HtmlDecode(mdQnA.AnsContent);
                    mdAnsDb.ContentText = Regex.Replace(HttpUtility.HtmlDecode(mdQnA.AnsContent), @"<(.|\n)*?>", "");
                    mdAnsDb.MQuestionID = mdQuesDb.ID;
                    _searchService.CreateAnswer(mdAnsDb);
                    _searchService.Save();

                    ApiQnaNLRService apiNLR = new ApiQnaNLRService();
                    string nlrQuesID = mdQuesDb.ID.ToString();
                    string nlrQuesContentText = mdQuesDb.ContentText;
                    string nlrAnsContentText = mdAnsDb.ContentText;
                    string nlrAnsContentHTML = mdAnsDb.ContentHTML;
                    string nlrAreaName = mdQnA.AreaName;
                    //apiNLR.AddQues(nlrQuesID, nlrQuesContentText, nlrAnsContentText, nlrAreaName, nlrAnsContentHTML);
                }
                response = request.CreateErrorResponse(HttpStatusCode.OK, "ok");
                return response;
            });
        }
    }
}
