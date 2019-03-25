using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Web.Models;
using BotProject.Model.Models;
using BotProject.Web.Infrastructure.Extensions;

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
                bool IsCreated = false;
				if(qGroupVm != null && qGroupVm.QuestionGroupViewModels != null
                                    && qGroupVm.QuestionGroupViewModels.Count() != 0)
                {
                    var lstQuesGroupVm = qGroupVm.QuestionGroupViewModels;
                    // nếu trường hợp cập nhật chỉ thêm các question group với id rỗng
                    if (qGroupVm.TypeAction == Common.CommonConstants.UpdateQnA)
                    {
                        lstQuesGroupVm = qGroupVm.QuestionGroupViewModels.Where(x=>x.ID == 0);
                    }
                    if(lstQuesGroupVm != null && lstQuesGroupVm.Count() != 0)
                    {
                        foreach (var itemQGroup in lstQuesGroupVm)
                        {
                            try
                            {
                                QuestionGroup qGroupDb = new QuestionGroup();
                                qGroupDb.UpdateQuestionGroup(itemQGroup);
                                _qnaService.AddQuesGroup(qGroupDb);
                                _qnaService.Save();
                                if (itemQGroup.QnAViewModel.QuestionViewModels != null &&
                                        itemQGroup.QnAViewModel.QuestionViewModels.Count() != 0)
                                {
                                    var lstQuestion = itemQGroup.QnAViewModel.QuestionViewModels;
                                    foreach (var itemQues in lstQuestion)
                                    {
                                        string code = qGroupDb.ID + Guid.NewGuid().ToString();
                                        Question quesDb = new Question();
                                        quesDb.UpdateQuestion(itemQues);
                                        quesDb.CodeSymbol = code;
                                        quesDb.QuestionGroupID = qGroupDb.ID;
                                        _qnaService.AddQuestion(quesDb);

                                        // is that star
                                        Question quesDbStar = new Question();
                                        quesDbStar.UpdateQuestionIsStar(itemQues);
                                        quesDbStar.CodeSymbol = code;
                                        quesDbStar.QuestionGroupID = qGroupDb.ID;
                                        _qnaService.AddQuestion(quesDbStar);
                                    }

                                    _qnaService.Save();
                                }
                                if (itemQGroup.QnAViewModel.AnswerViewModels != null &&
                                        itemQGroup.QnAViewModel.AnswerViewModels.Count() != 0)
                                {
                                    var lstAnswer = itemQGroup.QnAViewModel.AnswerViewModels;
                                    foreach (var itemAns in lstAnswer)
                                    {
                                        Answer ansDb = new Answer();
                                        ansDb.UpdateAnswer(itemAns);
                                        ansDb.QuestionGroupID = qGroupDb.ID;
                                        _qnaService.AddAnswer(ansDb);
                                    }
                                    _qnaService.Save();
                                }
                                IsCreated = true;
                            }
                            catch (Exception ex)
                            {
                                IsCreated = false;
                                response = request.CreateResponse(HttpStatusCode.BadGateway, IsCreated);
                            }
                        }
                    }                 
                }
                response = request.CreateResponse(HttpStatusCode.OK, IsCreated);	
				return response;
			});
		}

        [Route("getbybotqnanswerid")]
        [HttpGet]
        public HttpResponseMessage GetByBotQnAnswerId(HttpRequestMessage request, int botQnaID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstBotQna = _qnaService.GetListQuestionGroupByBotQnAnswerID(botQnaID).OrderByDescending(x=>x.CreatedDate).ToList();
                response = request.CreateResponse(HttpStatusCode.OK, lstBotQna);
                return response;
            });
        }

        [Route("deleteques")]
        [HttpPost]
        public HttpResponseMessage DeleteQuestion(HttpRequestMessage request, int questionID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var ques = _qnaService.DeleteQuestion(questionID);
                _qnaService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, ques);
                return response;
            });
        }


        [Route("deleteanswer")]
        [HttpPost]
        public HttpResponseMessage DeleteAnswer(HttpRequestMessage request, int answerID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var answer = _qnaService.DeleteAnswer(answerID);
                _qnaService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, answer);
                return response;
            });
        }

        [Route("deletequesgroup")]
        [HttpPost]
        public HttpResponseMessage DeleteQuesGroup(HttpRequestMessage request, int qGroupID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var qGroup = _qnaService.DeleteQuestionGroup(qGroupID);
                _qnaService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, qGroup);
                return response;
            });
        }

        [Route("updatequestion")]
        [HttpPost]
        public HttpResponseMessage UpdateQuestion(HttpRequestMessage request, QuestionViewModel quesVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    try
                    {
						var _lstQuesUpdate = _qnaService.GetListQuesCodeSymbol(quesVm.CodeSymbol).ToList();
						if(_lstQuesUpdate.Count != 0)
						{
							foreach(var item in _lstQuesUpdate)
							{
								if (item.IsThatStar == false)
								{
									item.ContentText = quesVm.ContentText.Trim();
								}
								else
								{
									item.ContentText = quesVm.ContentText.Trim() + " *";
								}
								_qnaService.UpdateQuestion(item);
							}
							_qnaService.Save();
						}

                        response = request.CreateResponse(HttpStatusCode.OK, "Success");
                    }
                    catch (Exception ex)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    }
                }
                return response;
            });
        }

		[Route("updateanswer")]
		[HttpPost]
		public HttpResponseMessage UpdateAnswer(HttpRequestMessage request, AnswerViewModel answerVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;
				Answer answerDb = new Answer();
				answerDb.UpdateAnswer(answerVm);
				_qnaService.Save();
				response = request.CreateResponse(HttpStatusCode.Created, answerDb);
				return response;
			});
		}

		[Route("addquestion")]
		[HttpPost]
		public HttpResponseMessage AddQuestion(HttpRequestMessage request, QuestionViewModel quesVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				string code = quesVm.QuestionGroupID + Guid.NewGuid().ToString();
				Question quesDb = new Question();
				quesDb.UpdateQuestion(quesVm);
				quesDb.CodeSymbol = code;
				quesDb.QuestionGroupID = quesVm.QuestionGroupID;
				_qnaService.AddQuestion(quesDb);

				// is that star
				Question quesDbStar = new Question();
				quesDbStar.UpdateQuestionIsStar(quesVm);
				quesDbStar.CodeSymbol = code;
				quesDbStar.QuestionGroupID = quesVm.QuestionGroupID;
				_qnaService.AddQuestion(quesDbStar);

				_qnaService.Save();
				response = request.CreateResponse(HttpStatusCode.Created, quesDbStar);
				return response;
			});
		}

	}
}
