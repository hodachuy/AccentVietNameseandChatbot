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
using BotProject.Web.Infrastructure.Extensions;

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

        [Route("getqnabyquesid")]
        [HttpGet]
        public HttpResponseMessage GetQnAByQuesId(HttpRequestMessage request, int quesId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string filter = "q.ID = " + quesId;
                var qnaDb = _searchService.GetListMdQnA(filter, "", 1, 1, null).ToList().FirstOrDefault();               
                response = request.CreateResponse(HttpStatusCode.OK, qnaDb);
                return response;
            });
        }

        [Route("createupdateqna")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateQnA(HttpRequestMessage request,ModuleQnAViewModel mdQnA )
        {
            return CreateHttpResponse(request,() => {
                HttpResponseMessage response = null;
                bool result = true;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                    return response;
                }
                MdQuestion mdQuesDb = new MdQuestion();
                MdAnswer mdAnsDb = new MdAnswer();
                ApiQnaNLRService apiNLR = new ApiQnaNLRService();
                if (mdQnA.QuesID == null)
                {
                    // add Ques
                    mdQuesDb.UpdateModuleQuestion(mdQnA);
                    _searchService.CreateQuestion(mdQuesDb);
                    _searchService.Save();
                    // add Ans
                    mdAnsDb.UpdateModuleAnswer(mdQnA);
                    mdAnsDb.MQuestionID = mdQuesDb.ID;
                    _searchService.CreateAnswer(mdAnsDb);

                }else
                {
                    // update Ques
                    mdQuesDb.UpdateModuleQuestion(mdQnA);
                    _searchService.UpdateQuestion(mdQuesDb);
                    // update Ans
                    mdAnsDb.UpdateModuleAnswer(mdQnA);
                    _searchService.UpdateAnswer(mdAnsDb);
                }
                _searchService.Save();

                // api training
                string nlrQuesID = mdQuesDb.ID.ToString();
                string nlrQuesContentText = mdQuesDb.ContentText;
                string nlrAnsContentText = mdAnsDb.ContentText;
                string nlrAnsContentHTML = mdAnsDb.ContentHTML;
                string nlrAreaName = mdQnA.AreaName;
                //apiNLR.AddQues(nlrQuesID, nlrQuesContentText, nlrAnsContentText, nlrAreaName, nlrAnsContentHTML);

                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }
    }
}
