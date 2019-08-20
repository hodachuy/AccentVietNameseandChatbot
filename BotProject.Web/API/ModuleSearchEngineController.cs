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
    /// <summary>
    /// MODULE QnAnswer SEARCH BY BOT
    /// </summary>
    [RoutePrefix("api/modulesearchengine")]
    public class ModuleSearchEngineController : ApiControllerBase
    {
        private IModuleSearchEngineService _moduleSearchEngineService;
        public ModuleSearchEngineController(IErrorService errorService, IModuleSearchEngineService moduleSearchEngineService) : base(errorService)
        {
            _moduleSearchEngineService = moduleSearchEngineService;
        }

        [Route("getallqna")]
        [HttpGet]
        public HttpResponseMessage GetListQuestionAnswer(HttpRequestMessage request, int page, int pageSize, string botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                string filter = "q.BotID = " + botId;
                var lstSearchQna = _moduleSearchEngineService.GetListMdQnA(filter, "", page, pageSize, null).ToList();
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
                var qnaDb = _moduleSearchEngineService.GetListMdQnA(filter, "", 1, 1, null).ToList().FirstOrDefault();
                response = request.CreateResponse(HttpStatusCode.OK, qnaDb);
                return response;
            });
        }

        [Route("createupdatearea")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateArea(HttpRequestMessage request, MdAreaViewModel mdAreaVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                    return response;
                }
                MdArea mdArea = new MdArea();
                if (mdAreaVm.ID == null)
                {
                    mdArea.UpdateMdArea(mdAreaVm);
                    _moduleSearchEngineService.CreateArea(mdArea);
                }
                else
                {
                    mdArea.UpdateMdArea(mdAreaVm);
                    _moduleSearchEngineService.UpdateArea(mdArea);
                }
                response = request.CreateResponse(HttpStatusCode.OK, mdArea);
                return response;
            });
        }


        [Route("getareabybotid")]
        [HttpGet]
        public HttpResponseMessage GetListAreaByBotId(HttpRequestMessage request, int botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var mdArea = _moduleSearchEngineService.GetListMdArea(botId).ToList();
                response = request.CreateResponse(HttpStatusCode.OK, mdArea);
                return response;
            });
        }

        [Route("createupdateqna")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateQnA(HttpRequestMessage request, ModuleQnAViewModel mdQnA)
        {
            return CreateHttpResponse(request, () =>
            {
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
                    _moduleSearchEngineService.CreateQuestion(mdQuesDb);
                    _moduleSearchEngineService.Save();
                    // add Ans
                    mdAnsDb.UpdateModuleAnswer(mdQnA);
                    mdAnsDb.MQuestionID = mdQuesDb.ID;
                    _moduleSearchEngineService.CreateAnswer(mdAnsDb);

                }
                else
                {
                    // update Ques
                    mdQuesDb.UpdateModuleQuestion(mdQnA);
                    _moduleSearchEngineService.UpdateQuestion(mdQuesDb);
                    // update Ans
                    mdAnsDb.UpdateModuleAnswer(mdQnA);
                    _moduleSearchEngineService.UpdateAnswer(mdAnsDb);
                }
                _moduleSearchEngineService.Save();

                // api training
                string nlrQuesID = mdQuesDb.ID.ToString();
                string nlrQuesContentText = mdQuesDb.ContentText;
                string nlrAnsContentText = mdAnsDb.ContentText;
                string nlrAnsContentHTML = mdAnsDb.ContentHTML;
                string nlrAreaName = mdQnA.AreaName;
                string nlrAreaID = (String.IsNullOrEmpty(mdQnA.AreaID.ToString()) == true ? "" : mdQnA.AreaID.ToString());
                string nlrBotID = mdQnA.BotID.ToString();
                //apiNLR.AddQues(nlrQuesID, nlrQuesContentText, nlrAnsContentText, nlrAreaName, nlrAnsContentHTML);

                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }
    }
}
