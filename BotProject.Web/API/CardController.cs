﻿using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Web.Models;
using BotProject.Model.Models;
using BotProject.Common;
using BotProject.Web.Infrastructure.Extensions;
using AutoMapper;

namespace BotProject.Web.API
{
    [RoutePrefix("api/card")]
    public class CardController : ApiControllerBase
    {
        private IErrorService _errorService;
        private ICardService _cardService;
        private IImageService _imageService;
        private ICommonCardService _commonCardService;
        public CardController(IErrorService errorService,
                            ICardService cardService,
                            IImageService imageService,
                            ICommonCardService commonCardService) : base(errorService)
        {
            _cardService = cardService;
            _imageService = imageService;
            _commonCardService = commonCardService;
            _errorService = errorService;
        }

        [Route("getbyid/{cardId:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int cardId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;




                return response;
            });
        }

        [Route("addupdate")]
        [HttpPost]
        public HttpResponseMessage AddUpdate(HttpRequestMessage request, CardViewModel cardVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                try
                {
                    ResultCard rs = new ResultCard();
                    Card cardDb = new Card();
                    if (cardVm.ID == null)
                    {
                        cardDb.UpdateCard(cardVm);
                        _cardService.Create(cardDb);
                        _cardService.Save();
                        cardDb.PatternText = CommonConstants.PostBackCard + cardDb.ID.ToString();//use to card #
                        rs.IsActionDb = true;
                    }
                    else
                    {
                        cardDb.UpdateCard(cardVm);
                        cardDb.ID = cardVm.ID.GetValueOrDefault();
                        _cardService.Update(cardDb);
                        _cardService.Save();
                        rs.IsActionDb = false;
                        // delete all
                        _commonCardService.DeleteFullContentCard(cardDb.ID);
                        _imageService.DeleteMutiImage(cardDb.ID);
                        //var lstImg = _imageService.GetByCardID(cardDb.ID);
                        //if(lstImg.Count() != 0)
                        //{

                        //}
                    }

                    //xác định index của template => đánh số index trên div class="content" card="text" 



                    if (cardVm.CardContents != null && cardVm.CardContents.Count() != 0)
                    {
                        foreach (var item in cardVm.CardContents)
                        {
                            var message = item.Message;
                            // Template generic
                            if (message.TemplateGenericGroupViewModel != null)
                            {
                                var temGnrGroupVm = message.TemplateGenericGroupViewModel;
                                TemplateGenericGroup tmpGrnGroupDb = new TemplateGenericGroup();
                                tmpGrnGroupDb.UpdateTemplateGenericGroup(temGnrGroupVm);
                                tmpGrnGroupDb.CardID = cardDb.ID;
                                _commonCardService.AddTempGnrGroup(tmpGrnGroupDb);
                                _commonCardService.Save();
                                if (temGnrGroupVm.TemplateGenericItemViewModels.Count() != 0)
                                {
                                    var lstTempGrnItemVm = temGnrGroupVm.TemplateGenericItemViewModels;
                                    foreach (var tempGnrItemVm in lstTempGrnItemVm)
                                    {
                                        TemplateGenericItem tempGnrItemDb = new TemplateGenericItem();
                                        tempGnrItemDb.UpdateTemplateGenericItem(tempGnrItemVm);
                                        tempGnrItemDb.TempGnrGroupID = tmpGrnGroupDb.ID;
                                        tempGnrItemDb.CardID = cardDb.ID;
                                        _commonCardService.AddTempGnrItem(tempGnrItemDb);
                                        _commonCardService.Save();
                                        if (tempGnrItemVm.ButtonLinkViewModels.Count() != 0)
                                        {
                                            var lstBtnLinkVm = tempGnrItemVm.ButtonLinkViewModels;
                                            foreach (var btnLinkVm in lstBtnLinkVm)
                                            {
                                                ButtonLink btnLinkDb = new ButtonLink();
                                                btnLinkDb.UpdateButtonLink(btnLinkVm);
                                                btnLinkDb.CardID = cardDb.ID;
                                                btnLinkDb.TempGnrItemID = tempGnrItemDb.ID;
                                                _commonCardService.AddButtonLink(btnLinkDb);
                                                _commonCardService.Save();
                                            }
                                        }
                                        if (tempGnrItemVm.ButtonPostbackViewModels.Count() != 0)
                                        {
                                            var lstBtnPostbackVm = tempGnrItemVm.ButtonPostbackViewModels;
                                            foreach (var btnPostbackVm in lstBtnPostbackVm)
                                            {
                                                ButtonPostback btnPostbackDb = new ButtonPostback();
                                                btnPostbackDb.UpdateButtonPostback(btnPostbackVm);
                                                btnPostbackDb.CardID = cardDb.ID;
                                                btnPostbackDb.TempGnrItemID = tempGnrItemDb.ID;
                                                _commonCardService.AddButtonPostback(btnPostbackDb);
                                                _commonCardService.Save();
                                            }
                                        }
                                    }
                                }
                            }
                            // Template Text
                            if (message.TemplateTextViewModel != null)
                            {
                                var tempTextVm = message.TemplateTextViewModel;
                                TemplateText tempTextDb = new TemplateText();
                                tempTextDb.UpdateTemplateText(tempTextVm);
                                tempTextDb.CardID = cardDb.ID;
                                _commonCardService.AddTempText(tempTextDb);
                                _commonCardService.Save();
                                if (tempTextVm.ButtonLinkViewModels.Count() != 0)
                                {
                                    var lstBtnLinkVm = tempTextVm.ButtonLinkViewModels;
                                    foreach (var btnLinkVm in lstBtnLinkVm)
                                    {
                                        ButtonLink btnLinkDb = new ButtonLink();
                                        btnLinkDb.UpdateButtonLink(btnLinkVm);
                                        btnLinkDb.CardID = cardDb.ID;
                                        btnLinkDb.TempTxtID = tempTextDb.ID;
                                        _commonCardService.AddButtonLink(btnLinkDb);
                                        _commonCardService.Save();
                                    }
                                }
                                if (tempTextVm.ButtonPostbackViewModels.Count() != 0)
                                {
                                    var lstBtnPostbackVm = tempTextVm.ButtonPostbackViewModels;
                                    foreach (var btnPostbackVm in lstBtnPostbackVm)
                                    {
                                        ButtonPostback btnPostbackDb = new ButtonPostback();
                                        btnPostbackDb.UpdateButtonPostback(btnPostbackVm);
                                        btnPostbackDb.CardID = cardDb.ID;
                                        btnPostbackDb.TempTxtID = tempTextDb.ID;
                                        _commonCardService.AddButtonPostback(btnPostbackDb);
                                        _commonCardService.Save();
                                    }
                                }
                            }
                            // Image
                            if (message.ImageViewModel != null)
                            {
                                var imgVm = message.ImageViewModel;
                                Image imgDb = new Image();
                                imgDb.CardID = cardDb.ID;
                                imgDb.BotID = cardVm.BotID;
                                imgDb.Url = imgVm.Url;
                                _commonCardService.AddImage(imgDb);
                                _commonCardService.Save();
                            }
                            if (cardVm.QuickReplyViewModels != null && cardVm.QuickReplyViewModels.Count() != 0)
                            {
                                var lstQuickReplyVm = cardVm.QuickReplyViewModels;
                                foreach (var itemQuickReplyVm in lstQuickReplyVm)
                                {
                                    QuickReply quickReplyDb = new QuickReply();
                                    quickReplyDb.UpdateQuickReply(itemQuickReplyVm);
                                    quickReplyDb.CardID = cardDb.ID;
                                    _commonCardService.AddQuickReply(quickReplyDb);
                                    _commonCardService.Save();
                                }
                            }
                        }
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, rs);
                }
                catch (Exception ex)
                {
                    Error err = new Error();
                    err.Message = ex.Message;
                    err.CreatedDate = DateTime.Now;
                    _errorService.Create(err);

                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                return response;
            });
        }

        public class ResultCard
        {
            public bool IsActionDb { set; get; }
            public Card Card { set; get; }
        }
    }
}
