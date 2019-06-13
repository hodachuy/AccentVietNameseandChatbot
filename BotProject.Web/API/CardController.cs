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
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace BotProject.Web.API
{
	[RoutePrefix("api/card")]
	public class CardController : ApiControllerBase
	{
		private IErrorService _errorService;
		private ICardService _cardService;
		private IImageService _imageService;
		private ICommonCardService _commonCardService;
		private IFileCardService _fileCardService;
        private IModuleKnowledegeService _mdKnowledgeService;
        public CardController(IErrorService errorService,
							ICardService cardService,
							IImageService imageService,
							ICommonCardService commonCardService,
							IFileCardService fileCardService) : base(errorService)
		{
			_cardService = cardService;
			_imageService = imageService;
			_commonCardService = commonCardService;
			_errorService = errorService;
			_fileCardService = fileCardService;

		}

		[Route("getbyid")]
		[HttpGet]
		public HttpResponseMessage GetById(HttpRequestMessage request, int cardId)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				var card = _commonCardService.GetFullDetailCard(cardId);
				response = request.CreateResponse(HttpStatusCode.OK, card);
				return response;
			});
		}

        [Route("getbygroupcard")]
        [HttpGet]
        public HttpResponseMessage GetAllByGroupCard(HttpRequestMessage request, int groupCardId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstCard = _cardService.GetListCardByGroupCardID(groupCardId);
                var lstCardVm = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(lstCard);            
                response = request.CreateResponse(HttpStatusCode.OK, lstCardVm);
                return response;
            });
        }

        [Route("getbybot")]
        [HttpGet]
        public HttpResponseMessage GetAllByBot(HttpRequestMessage request, int botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstCard = _cardService.GetListCardByBotID(botId);
                var lstCardVm = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(lstCard);
                response = request.CreateResponse(HttpStatusCode.OK, lstCardVm);
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
						_cardService.Update(cardDb);
						_cardService.Save();
						rs.IsActionDb = true;

						// create file card aiml
						//string pathFolderAIML = ConfigurationManager.AppSettings["AIMLPath"] + "\\" + "User_" + cardVm.UserID + "_BotID_" + cardVm.BotID;
                        string pathFolderAIML = PathServer.PathAIML + "User_" + cardVm.UserID + "_BotID_" + cardVm.BotID;
                        string nameFolderAIML = "Card_ID_" + cardDb.ID + "_" + cardDb.Alias + ".aiml";
						string pathString = System.IO.Path.Combine(pathFolderAIML, nameFolderAIML);
						if (!System.IO.File.Exists(pathString))
						{
							try
							{
								//Open the File
								StreamWriter sw = new StreamWriter(pathString, true, Encoding.UTF8);
								//sw.WriteLine("Hello World!!");
								sw.Close();
							}
							catch (Exception e)
							{

							}
							finally
							{

							}

						}
						else
						{
							string message = "Không tìm thấy đường dẫn " + ConfigurationManager.AppSettings["AIMLPath"] + "\\" + "User_" + cardVm.UserID + "_BotID_" + cardVm.BotID;
							response = request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
							return response;
						}

					}
					else
					{
						cardDb.UpdateCard(cardVm);
						cardDb.PatternText = CommonConstants.PostBackCard + cardVm.ID.ToString();//use to card #
						cardDb.ID = cardVm.ID.GetValueOrDefault();
						_cardService.Update(cardDb);
						_cardService.Save();
						rs.IsActionDb = false;
						// delete all
						_commonCardService.DeleteFullContentCard(cardDb.ID);
						//_imageService.DeleteMutiImage(cardDb.ID);
						//if (cardVm.FileAttachs != null && cardVm.FileAttachs.Count != 0)
						//{
						//    string pathImgServer = ConfigurationManager.AppSettings["ImagePath"];
						//    string[] Files = Directory.GetFiles(pathImgServer);
						//    foreach (var item in cardVm.FileAttachs)
						//    {
						//        var fileFileCard = _fileCardService.Delete(item.attachment_id);
						//        string fileName = Regex.Replace(item.attachment_url, "File/Images/Card/", "");
						//        foreach (string file in Files)
						//        {
						//            if (file.ToUpper().Contains(fileName.ToUpper()))
						//            {
						//                File.Delete(file);
						//            }
						//        }
						//    }
						//    _fileCardService.Save();
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
                                        if (tempGnrItemVm.ButtonModuleViewModels.Count() != 0)
                                        {
                                            var lstBtnModuleVm = tempGnrItemVm.ButtonModuleViewModels;
                                            foreach (var btnModuleVm in lstBtnModuleVm)
                                            {
                                                ButtonModule btnModuleDb = new ButtonModule();
                                                btnModuleDb.UpdateButtonModule(btnModuleVm);
                                                btnModuleDb.CardID = cardDb.ID;
                                                btnModuleDb.TempGnrItemID = tempGnrItemVm.ID;
                                                if(btnModuleVm.ModuleKnowledgeID != null && btnModuleVm.ModuleKnowledgeID != 0)
                                                {
                                                    btnModuleDb.ModuleKnowledgeID = btnModuleVm.ModuleKnowledgeID;
                                                    btnModuleDb.Payload = btnModuleVm.Payload;// + "_" + btnModuleVm.ModuleKnowledgeID
                                                }

                                                _commonCardService.AddButtonModule(btnModuleDb);
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
                                if (tempTextVm.ButtonModuleViewModels.Count() != 0)
                                {
                                    var lstBtnModuleVm = tempTextVm.ButtonModuleViewModels;
                                    foreach (var btnModuleVm in lstBtnModuleVm)
                                    {
                                        ButtonModule btnModuleDb = new ButtonModule();
                                        btnModuleDb.UpdateButtonModule(btnModuleVm);
                                        btnModuleDb.CardID = cardDb.ID;
                                        btnModuleDb.TempTxtID = tempTextDb.ID;
                                        if (btnModuleVm.ModuleKnowledgeID != null && btnModuleVm.ModuleKnowledgeID != 0)
                                        {
                                            btnModuleDb.ModuleKnowledgeID = btnModuleVm.ModuleKnowledgeID;
                                            btnModuleDb.Payload = btnModuleVm.Payload;// + "_" + btnModuleVm.ModuleKnowledgeID
                                        }
                                        _commonCardService.AddButtonModule(btnModuleDb);
                                        _commonCardService.Save();

                                        // update button id tới module med get info patient
                                        if (btnModuleVm.ModuleKnowledgeID != null && btnModuleVm.ModuleKnowledgeID != 0)
                                        {
                                            var mdMedGetInfoPatientDb = _mdKnowledgeService.GetByMdMedInfoPatientID(btnModuleVm.ModuleKnowledgeID ?? default(int));
                                            mdMedGetInfoPatientDb.ButtonModuleID = btnModuleDb.ID;
                                            _mdKnowledgeService.UpdateMdKnowledfeMedInfoPatient(mdMedGetInfoPatientDb);
                                            _mdKnowledgeService.Save();
                                        }
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
                                imgDb.Index = imgVm.Index;
								_commonCardService.AddImage(imgDb);
								_commonCardService.Save();
							}
                            // module follow card
                            if (message.ModuleFollowCardViewModel != null)
                            {
                                var mdFollowCardVm = message.ModuleFollowCardViewModel;
                                ModuleFollowCard mdFCardDb = new ModuleFollowCard();
                                mdFCardDb.CardID = cardDb.ID;
                                mdFCardDb.BotID = cardVm.BotID;
                                mdFCardDb.PartternText = mdFollowCardVm.PartternText;//postback_moudle_phone...//
                                if (mdFollowCardVm.PartternText != "post_back_med_get_info_patient")
                                {
                                    mdFCardDb.ModuleInfoPatientID = 0;
                                    if(mdFollowCardVm.ModuleInfoPatientID != null && mdFollowCardVm.ModuleInfoPatientID != 0)
                                    {
                                        _mdKnowledgeService.DeleteMdMedInfoPatient(mdFollowCardVm.ModuleInfoPatientID ?? default(int));
                                    }
                                }else
                                {
                                    mdFCardDb.ModuleInfoPatientID = mdFollowCardVm.ModuleInfoPatientID;
                                }
                                mdFCardDb.Index = mdFollowCardVm.Index;
                                _commonCardService.AddModuleFollowCard(mdFCardDb);
                                _commonCardService.Save();
                                // update
                                if (mdFollowCardVm.PartternText == "post_back_med_get_info_patient")
                                {
                                    var mdMedGetInfoPatientDb = _mdKnowledgeService.GetByMdMedInfoPatientID(mdFCardDb.ModuleInfoPatientID ?? default(int));
                                    mdMedGetInfoPatientDb.ModuleFollowCardID = mdFCardDb.ID;
                                    _mdKnowledgeService.UpdateMdKnowledfeMedInfoPatient(mdMedGetInfoPatientDb);
                                    _mdKnowledgeService.Save();
                                }
                            }

                            // trả lời nhanh
                            if (cardVm.QuickReplyViewModels != null && cardVm.QuickReplyViewModels.Count() != 0)
							{
								var lstQuickReplyVm = cardVm.QuickReplyViewModels;
								foreach (var itemQuickReplyVm in lstQuickReplyVm)
								{
                                    BotProject.Model.Models.QuickReply quickReplyDb = new BotProject.Model.Models.QuickReply();
									quickReplyDb.UpdateQuickReply(itemQuickReplyVm);
									quickReplyDb.CardID = cardDb.ID;
									_commonCardService.AddQuickReply(quickReplyDb);
									_commonCardService.Save();
								}
							}
						}
					}

					rs.Card = cardDb;
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

		[Route("getaimlcard")]
		[HttpGet]
		public HttpResponseMessage GetAimlCard(HttpRequestMessage request, int cardId, string userId)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				var card = _commonCardService.GetFullDetailCard(cardId);
                // open file card aiml
                //string pathFolderAIML = ConfigurationManager.AppSettings["AIMLPath"] + "\\" + "User_" + userId + "_BotID_" + card.BotID;
                string pathFolderAIML = PathServer.PathAIML + "User_" + userId + "_BotID_" + card.BotID;
                string nameFolderAIML = "Card_ID_" + card.ID + "_" + card.Alias + ".aiml";
				string pathString = System.IO.Path.Combine(pathFolderAIML, nameFolderAIML);
				if (System.IO.File.Exists(pathString))
				{
					File.WriteAllText(pathString, string.Empty);
					try
					{
						StreamWriter sw = new StreamWriter(pathString, true, Encoding.UTF8);
						sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
						sw.WriteLine("<aiml>");
						sw.WriteLine("<category>");
						sw.WriteLine("<pattern>"+card.PatternText+"</pattern>");
						sw.WriteLine("<template>");
						if(card.TemplateTexts != null && card.TemplateTexts.Count() != 0)
						{
							var lstTemplateText = card.TemplateTexts;
							foreach(var item in lstTemplateText)
							{
								// text
								sw.WriteLine(""+item.Text+"");
								if (item.ButtonPostbacks != null && item.ButtonPostbacks.Count() != 0)
								{
									var lstButtonPostbacks = item.ButtonPostbacks;
									foreach(var itemBtnPostback in lstButtonPostbacks)
									{
										sw.WriteLine("<button>");
										sw.WriteLine("<text>"+itemBtnPostback.Title+"</text>");
										sw.WriteLine("<menu>" + itemBtnPostback.Payload + "</menu>");
										sw.WriteLine("</button>");
									}
								}
								if (item.ButtonLinks != null && item.ButtonLinks.Count() != 0)
								{
									var lstButtonLinks = item.ButtonLinks;
									foreach (var itemBtnLink in lstButtonLinks)
									{
										sw.WriteLine("<button>");
										sw.WriteLine("<text>" + itemBtnLink.Title + "</text>");
										sw.WriteLine("<url>" + itemBtnLink.Url + "</url>");
										sw.WriteLine("</button>");
									}
								}
                                if (item.ButtonModules != null && item.ButtonModules.Count() != 0)
                                {
                                    var lstButtonModules = item.ButtonModules;
                                    foreach (var itemBtnModule in lstButtonModules)
                                    {
                                        sw.WriteLine("<button>");
                                        sw.WriteLine("<text>" + itemBtnModule.Title + "</text>");
                                        if(itemBtnModule.ModuleKnowledgeID != null && itemBtnModule.ModuleKnowledgeID != 0)
                                        {
                                            sw.WriteLine("<module>" + itemBtnModule.Payload + "_" + itemBtnModule.ModuleKnowledgeID + "</module>");
                                        }
                                        else
                                        {
                                            sw.WriteLine("<module>" + itemBtnModule.Payload + "</module>");
                                        }
                                        sw.WriteLine("</button>");
                                    }
                                }
                            }
						}
						if(card.TemplateGenericGroups != null && card.TemplateGenericGroups.Count() != 0)
						{
                            var lstTempGnrGroup = card.TemplateGenericGroups;
                            foreach(var item in lstTempGnrGroup)
                            {
                                if(item.TemplateGenericItems != null && item.TemplateGenericItems.Count() != 0)
                                {
                                    var lstTemGnrItem = item.TemplateGenericItems;
                                    StringBuilder sb = new StringBuilder();
                                    foreach(var itemGnr in lstTemGnrItem)
                                    {
                                        sb.AppendLine("<card>");
                                        if (!String.IsNullOrEmpty(itemGnr.Image))
                                        {
                                            sb.AppendLine("<image>" + itemGnr.Image + "</image>");
                                        }
                                        sb.AppendLine("<title>"+itemGnr.Title+"</title>");
                                        sb.AppendLine("<subtitle>"+itemGnr.SubTitle+"</subtitle>");
                                        sb.AppendLine("<link>");
                                        sb.AppendLine("<text>"+ itemGnr.Url+"</text>");
                                        sb.AppendLine("<url>" + itemGnr.Url + "</url>");
                                        sb.AppendLine("</link>");
                                        if(itemGnr.ButtonPostbacks != null && itemGnr.ButtonPostbacks.Count() != 0)
                                        {
                                            var lstButtonPostbacks = itemGnr.ButtonPostbacks;
                                            foreach (var itemBtnPostback in lstButtonPostbacks)
                                            {
                                                sb.AppendLine("<button>");
                                                sb.AppendLine("<text>" + itemBtnPostback.Title + "</text>");
                                                sb.AppendLine("<menu>" + itemBtnPostback.Payload + "</menu>");
                                                sb.AppendLine("</button>");
                                            }
                                        }
                                        if (itemGnr.ButtonLinks != null && itemGnr.ButtonLinks.Count() != 0)
                                        {
                                            var lstButtonLinks = itemGnr.ButtonLinks;
                                            foreach (var itemBtnLink in lstButtonLinks)
                                            {
                                                sb.AppendLine("<button>");
                                                sb.AppendLine("<text>" + itemBtnLink.Title + "</text>");
                                                sb.AppendLine("<url>" + itemBtnLink.Url + "</url>");
                                                sb.AppendLine("</button>");
                                            }
                                        }
                                        if (itemGnr.ButtonModules != null && itemGnr.ButtonModules.Count() != 0)
                                        {
                                            var lstButtonModules = itemGnr.ButtonModules;
                                            foreach (var itemBtnModule in lstButtonModules)
                                            {
                                                sb.AppendLine("<button>");
                                                sb.AppendLine("<text>" + itemBtnModule.Title + "</text>");
                                                if (itemBtnModule.ModuleKnowledgeID != null && itemBtnModule.ModuleKnowledgeID != 0)
                                                {
                                                    sw.WriteLine("<module>" + itemBtnModule.Payload + "_" + itemBtnModule.ModuleKnowledgeID + "</module>");
                                                }
                                                else
                                                {
                                                    sw.WriteLine("<module>" + itemBtnModule.Payload + "</module>");
                                                }
                                                sb.AppendLine("</button>");
                                            }
                                        }
                                        sb.AppendLine("</card>");
                                    }

                                    if (lstTemGnrItem.Count() > 1)
                                    {
                                        //carousel
                                        sw.WriteLine("<carousel>");
                                        sw.WriteLine(sb.ToString());
                                        sw.WriteLine("</carousel>");
                                    }else
                                    {
                                        //card
                                        sw.WriteLine(sb.ToString());
                                    }
                                }
                            }
                        }
                        if(card.Images != null && card.Images.Count() != 0)
                        {
                            foreach(var itemImg in card.Images)
                            {
                                sw.WriteLine("<image>"+itemImg.Url+"</image>");
                            }
                        }
                        if (card.ModuleFollowCards != null && card.ModuleFollowCards.Count() != 0)
                        {
                            foreach (var itemMdFollowCards in card.ModuleFollowCards)
                            {
                                string patternText = itemMdFollowCards.PartternText;
                                if(itemMdFollowCards.ModuleInfoPatientID != null && itemMdFollowCards.ModuleInfoPatientID != 0)
                                {
                                    patternText = patternText + "_" + itemMdFollowCards.ModuleInfoPatientID;
                                }
                                sw.WriteLine(patternText);
                            }
                        }
                        if (card.QuickReplys != null && card.QuickReplys.Count() != 0)
                        {
                            var lstQuickReply = card.QuickReplys;
                            foreach(var itemQ in lstQuickReply)
                            {
                                sw.WriteLine("<button>");
                                sw.WriteLine("<text>" + itemQ.Title + "</text>");
                                sw.WriteLine("<postback>" + itemQ.Payload + "</postback>");
                                sw.WriteLine("</button>");
                            }
                        }
						sw.WriteLine("</template>");
						sw.WriteLine("</category>");
						sw.WriteLine("</aiml>");
						sw.Close();
					}
					catch (Exception e)
					{

					}
					finally
					{

					}

				}
				response = request.CreateResponse(HttpStatusCode.OK, card);
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
