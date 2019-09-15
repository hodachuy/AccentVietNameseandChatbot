using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Models;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Web.Infrastructure.Extensions;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using BotProject.Common;

namespace BotProject.Web.API
{
    [RoutePrefix("api/module")]
    public class ModuleController : ApiControllerBase
    {
        private IModuleService _moduleService;
        private IMdVoucherService _mdVoucherService;
        private IMdPhoneService _mdPhoneService;
        private IMdEmailService _mdEmailService;
        private IMdAgeService _mdAgeService;
        private IMdSearchService _mdSearchService;
        private IModuleKnowledegeService _mdKnowledegeService;
        private IMdSearchCategoryService _mdSearchCategoryService;
        public ModuleController(IErrorService errorService,
            IMdVoucherService mdVoucherService,
            IModuleService moduleService,
            IMdPhoneService mdPhoneService,
            IMdEmailService mdEmailService,
            IMdSearchService mdSearchService,
            IModuleKnowledegeService mdKnowledegeService,
            IMdSearchCategoryService mdSearchCategoryService,
            IMdAgeService mdAgeService) : base(errorService)
        {
            _mdVoucherService = mdVoucherService;
            _moduleService = moduleService;
            _mdPhoneService = mdPhoneService;
            _mdEmailService = mdEmailService;
            _mdAgeService = mdAgeService;
            _mdSearchService = mdSearchService;
            _mdKnowledegeService = mdKnowledegeService;
            _mdSearchCategoryService = mdSearchCategoryService;
        }
        [Route("getbybotid")]
        [HttpGet]
        public HttpResponseMessage GetAllModuleByBotID(HttpRequestMessage request, int botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstModule = _moduleService.GetAllModuleByBotID(botId);
                response = request.CreateResponse(HttpStatusCode.OK, lstModule);
                return response;
            });
        }

        [Route("getbymoduleid")]
        [HttpGet]
        public HttpResponseMessage GetSingleByID(HttpRequestMessage request, int moduleId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _moduleService.GetByID(moduleId);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ModuleViewModel moduleVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Module moduleDb = new Module();
                moduleDb.UpdateModule(moduleVm);
                _moduleService.Create(moduleDb);
                _moduleService.Save();

                if(moduleVm.Payload == Common.CommonConstants.ModulePhone)
                {
                    MdPhone mdPhone = new MdPhone();
                    mdPhone.BotID = moduleVm.BotID;
                    mdPhone.MessageStart = "Vui lòng nhập số điện thoại của bạn hoặc chọn bên dưới nếu có.";
                    mdPhone.MessageError = "Số điện thoại không hợp lệ, vui lòng nhập lại!";
                    mdPhone.MessageEnd = "Chúng tôi đã nhận được số điện thoại của bạn. Cảm ơn!";
                    mdPhone.ModuleID = moduleDb.ID;
                    //mdPhone.CardPayloadID = null;
                    //mdPhone.Payload = "";
                    mdPhone.Title = "Xử lý số điện thoại";
                    mdPhone.DictionaryKey = "phone";
                    mdPhone.DictionaryValue = "false";

                    _mdPhoneService.Create(mdPhone);
                    _mdPhoneService.Save();
                }
                if (moduleVm.Payload == Common.CommonConstants.ModuleEmail)
                {
                    MdEmail mdEmail = new MdEmail();
                    mdEmail.BotID = moduleVm.BotID;
                    mdEmail.MessageStart = "Vui lòng nhập email của bạn hoặc chọn bên dưới nếu có.";
                    mdEmail.MessageError = "Địa chỉ email không hợp lệ, vui lòng nhập lại!";
                    mdEmail.MessageEnd = "Cảm ơn bạn đã cung cấp thông tin!";
                    mdEmail.ModuleID = moduleDb.ID;
                    //mdPhone.CardPayloadID = null;
                    //mdPhone.Payload = "";
                    mdEmail.Title = "Xử lý email";
                    mdEmail.DictionaryKey = "email";
                    mdEmail.DictionaryValue = "false";

                    _mdEmailService.Create(mdEmail);
                    _mdEmailService.Save();
                }
                if (moduleVm.Payload == Common.CommonConstants.ModuleAge)
                {
                    MdAge mdAge = new MdAge();
                    mdAge.BotID = moduleVm.BotID;
                    mdAge.MessageStart = "Bạn vui lòng cho tôi biết độ tuổi của bạn.";
                    mdAge.MessageError = "Tôi không nghĩ đó là số tuổi, bạn vui lòng nhập vào chữ số.";
                    mdAge.MessageEnd = "Cảm ơn bạn, chúng tôi đã tiếp nhận thông tin thành công!";
                    mdAge.ModuleID = moduleDb.ID;
                    //mdPhone.CardPayloadID = null;
                    //mdPhone.Payload = "";
                    mdAge.Title = "Xử lý tuổi";
                    mdAge.DictionaryKey = "age";
                    mdAge.DictionaryValue = "false";

                    _mdAgeService.Create(mdAge);
                    _mdAgeService.Save();
                }
                //if (moduleVm.Payload == Common.CommonConstants.ModuleVoucher)
                //{
                //    MdVoucher mdVoucher = new MdVoucher();
                //    mdVoucher.BotID = moduleVm.BotID;
                //    mdVoucher.MessageStart = "Bạn vui lòng nhập số điện thoại để nhận voucher.";
                //    mdVoucher.MessageError = "Số điện thoại không đúng, bạn vui lòng nhập lại.";
                //    mdVoucher.MessageEnd = "Cảm ơn bạn, chúng đã tiếp nhận thông tin thành công!";
                //    mdVoucher.ModuleID = moduleDb.ID;
                //    //mdPhone.CardPayloadID = null;
                //    //mdPhone.Payload = "";
                //    mdVoucher.Title = "Xử lý voucher";
                //    mdVoucher.DictionaryKey = "voucher";
                //    mdVoucher.DictionaryValue = "false";

                //    _mdVoucherService.Create(mdVoucher);
                //    _mdVoucherService.Save();
                //}

                response = request.CreateResponse(HttpStatusCode.OK, moduleDb);
                return response;
            });
        }

        #region MODULE VOUCHER
        [Route("getmdvoucher")]
        [HttpGet]
        public HttpResponseMessage GetModuleVoucherByBotID(HttpRequestMessage request, int mdVoucherID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdVoucherService.GetByID(mdVoucherID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        [Route("addmdvoucher")]
        [HttpPost]
        public HttpResponseMessage AddModuleVoucher(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var mdVoucherJson = HttpContext.Current.Request.Unvalidated.Form["mdVoucher"];
                if (mdVoucherJson == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Vui lòng cung cấp dữ liệu.");
                    return response;
                }
                var mdVoucherVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<MdVoucher>(mdVoucherJson);

                MdVoucher mdVoucherDb = new MdVoucher();
                mdVoucherDb.Title = mdVoucherVm.Title;
                mdVoucherDb.MessageStart = mdVoucherVm.MessageStart;
                mdVoucherDb.StartDate = mdVoucherVm.StartDate;
                mdVoucherDb.ExpirationDate = mdVoucherVm.ExpirationDate;
                mdVoucherDb.CardPayloadID = mdVoucherVm.CardPayloadID;
                mdVoucherDb.TitlePayload = mdVoucherVm.TitlePayload;
                mdVoucherDb.MessageError = "Số điện thoại không đúng, bạn vui lòng nhập lại.";
                mdVoucherDb.MessageEnd = "Cảm ơn bạn, chúng tôi đã tiếp nhận thông tin thành công!";
                mdVoucherDb.Code = mdVoucherVm.Code;
                mdVoucherDb.Payload = "";
                if (mdVoucherVm.CardPayloadID != null && mdVoucherVm.CardPayloadID != 0)
                {
                    mdVoucherDb.Payload = "postback_card_" + mdVoucherVm.CardPayloadID;
                }

                var files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count != 0)
                {
                    var file = files[0];
                    Guid id = Guid.NewGuid();
                    string extentsion = new FileInfo(file.FileName).Extension.ToLower();
                    string fileName = id + "-" + new FileInfo(file.FileName).Name;

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/File/Images/")
                    + "Voucher" + "/" + fileName));

                    mdVoucherDb.Image = "File/Images/Voucher/" + fileName;
                }

                _mdVoucherService.Create(mdVoucherDb);
                _mdVoucherService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, mdVoucherDb);
                return response;
            });
        }

        [Route("updatemdvoucher")]
        [HttpPost]
        public HttpResponseMessage UpdateModuleVoucher(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var mdVoucherJson = HttpContext.Current.Request.Unvalidated.Form["mdVoucher"];
                if(mdVoucherJson == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Vui lòng cung cấp dữ liệu.");
                    return response;
                }
                var mdVoucherVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<MdVoucher>(mdVoucherJson);

                var mdVoucherDb = _mdVoucherService.GetByID(mdVoucherVm.ID);
                mdVoucherDb.Title = mdVoucherVm.Title;
                mdVoucherDb.MessageStart = mdVoucherVm.MessageStart;
                mdVoucherDb.StartDate = mdVoucherVm.StartDate;
                mdVoucherDb.ExpirationDate = mdVoucherVm.ExpirationDate;
                mdVoucherDb.CardPayloadID = mdVoucherVm.CardPayloadID;
                mdVoucherDb.TitlePayload = mdVoucherVm.TitlePayload;
                mdVoucherDb.Code = mdVoucherVm.Code;
                mdVoucherDb.Payload = "";
                if (mdVoucherVm.CardPayloadID != null && mdVoucherVm.CardPayloadID != 0)
                {
                    mdVoucherDb.Payload = "postback_card_" + mdVoucherVm.CardPayloadID;
                }

                var files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count != 0)
                {
                    var file = files[0];
                    Guid id = Guid.NewGuid();
                    string extentsion = new FileInfo(file.FileName).Extension.ToLower();
                    string fileName = id + "-" + new FileInfo(file.FileName).Name;

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/File/Images/")
                    + "Voucher" + "/" + fileName));

                    mdVoucherDb.Image = "File/Images/Voucher/" + fileName;
                }

                _mdVoucherService.Update(mdVoucherDb);
                _mdVoucherService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, mdVoucherDb);
                return response;
            });
        }
        #endregion

        #region MODULE PHONE
        [Route("getmdphone")]
        [HttpGet]
        public HttpResponseMessage GetModulePhoneByBotID(HttpRequestMessage request, int botID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdPhoneService.GetByBotID(botID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }
        [Route("updatemdphone")]
        [HttpPost]
        public HttpResponseMessage CreateModulePhone(HttpRequestMessage request, MdPhone mdPhone)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var module = _mdPhoneService.GetByBotID(mdPhone.BotID);
                module.MessageStart = mdPhone.MessageStart;
                module.MessageError = mdPhone.MessageError;
                module.MessageEnd = mdPhone.MessageEnd;
                module.CardPayloadID = mdPhone.CardPayloadID;
                if(mdPhone.CardPayloadID != null && mdPhone.CardPayloadID != 0)
                {
                    module.Payload = "postback_card_" + mdPhone.CardPayloadID;
                }
                else
                {
                    module.Payload = "";
                }

                _mdPhoneService.Update(module);
                _mdPhoneService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        #endregion

        #region MODULE EMAIL
        [Route("getmdemail")]
        [HttpGet]
        public HttpResponseMessage GetModuleEmailByBotID(HttpRequestMessage request, int botID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdEmailService.GetByBotID(botID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }
        [Route("updatemdemail")]
        [HttpPost]
        public HttpResponseMessage CreateModuleEmail(HttpRequestMessage request, MdEmail mdEmail)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var module = _mdEmailService.GetByBotID(mdEmail.BotID);
                module.MessageStart = mdEmail.MessageStart;
                module.MessageError = mdEmail.MessageError;
                module.MessageEnd = mdEmail.MessageEnd;
                module.CardPayloadID = mdEmail.CardPayloadID;
                if (mdEmail.CardPayloadID != null && mdEmail.CardPayloadID != 0)
                {
                    module.Payload = "postback_card_" + mdEmail.CardPayloadID;
                }
                else
                {
                    module.Payload = "";
                }

                _mdEmailService.Update(module);
                _mdEmailService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        #endregion

        #region MODULE AGE
        [Route("getmdage")]
        [HttpGet]
        public HttpResponseMessage GetModuleAgeByBotID(HttpRequestMessage request, int botID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdAgeService.GetByBotID(botID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }
        [Route("updatemdage")]
        [HttpPost]
        public HttpResponseMessage CreateModuleAge(HttpRequestMessage request, MdAge mdAge)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var module = _mdAgeService.GetByBotID(mdAge.BotID);
                module.MessageStart = mdAge.MessageStart;
                module.MessageError = mdAge.MessageError;
                module.MessageEnd = mdAge.MessageEnd;
                module.CardPayloadID = mdAge.CardPayloadID;
                if (mdAge.CardPayloadID != null && mdAge.CardPayloadID != 0)
                {
                    module.Payload = "postback_card_" + mdAge.CardPayloadID;
                }
                else
                {
                    module.Payload = "";
                }

                _mdAgeService.Update(module);
                _mdAgeService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        #endregion

        #region MDMEDGETINFOPATIENT
        [Route("getmdmedgetinfopatient")]
        [HttpGet]
        public HttpResponseMessage GetMdMedGetInfoPatient(HttpRequestMessage request, int mdInfPatientID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdKnowledegeService.GetByMdMedInfoPatientID(mdInfPatientID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        [Route("addmdmedgetinfopatient")]
        [HttpPost]
        public HttpResponseMessage AddMdMedInfoPatient(HttpRequestMessage request, ModuleKnowledgeMedInfoPatientViewModel mdKnowledgePatientVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ModuleKnowledgeMedInfoPatient mdKnowledgePatientDb = new ModuleKnowledgeMedInfoPatient();
                mdKnowledgePatientDb.BotID = mdKnowledgePatientVm.BotID;
                mdKnowledgePatientDb.CardPayloadID = mdKnowledgePatientVm.CardPayloadID;
                mdKnowledgePatientDb.Payload = mdKnowledgePatientVm.Payload;
                mdKnowledgePatientDb.MessageEnd = mdKnowledgePatientVm.MessageEnd;
                mdKnowledgePatientDb.Title = mdKnowledgePatientVm.Title;
                mdKnowledgePatientDb.OptionText = mdKnowledgePatientVm.OptionText;

                //mdKnowledgePatientDb.Key = "med_get_info_patinent_ID_index";

                _mdKnowledegeService.Add(mdKnowledgePatientDb);
                _mdKnowledegeService.Save();

                response = request.CreateResponse(HttpStatusCode.OK, mdKnowledgePatientDb);
                return response;
            });
        }

        [Route("updatemdmedgetinfopatient")]
        [HttpPost]
        public HttpResponseMessage UpdateMdMedInfoPatient(HttpRequestMessage request, ModuleKnowledgeMedInfoPatientViewModel mdKnowledgePatientVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                try
                {
                    var moduleInfoPatientDb = _mdKnowledegeService.GetByMdMedInfoPatientID(mdKnowledgePatientVm.ID);

                    moduleInfoPatientDb.BotID = mdKnowledgePatientVm.BotID;
                    moduleInfoPatientDb.CardPayloadID = mdKnowledgePatientVm.CardPayloadID;
                    moduleInfoPatientDb.Payload = mdKnowledgePatientVm.Payload;
                    moduleInfoPatientDb.MessageEnd = mdKnowledgePatientVm.MessageEnd;
                    moduleInfoPatientDb.Title = mdKnowledgePatientVm.Title;
                    moduleInfoPatientDb.OptionText = mdKnowledgePatientVm.OptionText;
                    //mdKnowledgePatientDb.Key = "med_get_info_patinent_ID_index";
                    _mdKnowledegeService.UpdateMdKnowledfeMedInfoPatient(moduleInfoPatientDb);
                    _mdKnowledegeService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, moduleInfoPatientDb);
                }
                catch(Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.BadGateway);
                }           
                return response;
            });
        }

        #endregion

        #region MODULE SEARCH
        [Route("getmdsearchcategory")]
        [HttpGet]
        public HttpResponseMessage GetModuleSearchCategory(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstMdSearchCategory = _mdSearchCategoryService.GetListMdSearchCategory();
                response = request.CreateResponse(HttpStatusCode.OK, lstMdSearchCategory);
                return response;
            });
        }


        [Route("getmdsearch")]
        [HttpGet]
        public HttpResponseMessage GetModuleSearchByAPI(HttpRequestMessage request, int mdSearchID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdSearchService.GetByID(mdSearchID);
                response = request.CreateResponse(HttpStatusCode.OK, module);
                return response;
            });
        }

        [Route("addmdsearch")]
        [HttpPost]
        public HttpResponseMessage AddModuleSearch(HttpRequestMessage request, MdSearchViewModel mdSearchVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                try
                {
                    MdSearch mdSearchDb = new MdSearch();
                    mdSearchDb.BotID = mdSearchVm.BotID;
                    mdSearchDb.Title = mdSearchVm.Title;
                    mdSearchDb.Payload = mdSearchVm.Payload;
                    mdSearchDb.CardPayloadID = mdSearchVm.CardPayloadID;
                    mdSearchDb.UrlAPI = mdSearchVm.UrlAPI;
                    mdSearchDb.TitlePayload = mdSearchVm.TitlePayload;
                    mdSearchDb.MethodeAPI = mdSearchVm.MethodeAPI;
                    if (!String.IsNullOrEmpty(mdSearchVm.KeyCodeAPI) && !String.IsNullOrEmpty(mdSearchVm.KeyNameAPI))
                    {
                        mdSearchDb.KeyAPI = mdSearchVm.KeyNameAPI + ":" + mdSearchVm.KeyCodeAPI;
                    }

                    mdSearchDb.ParamAPI = mdSearchVm.ParamAPI;
                    mdSearchDb.MessageStart = mdSearchVm.MessageStart;
                    mdSearchDb.MessageError = mdSearchVm.MessageError;
                    mdSearchDb.MessageEnd = mdSearchDb.MessageEnd;
                    mdSearchDb.ID = mdSearchVm.ID;
                    mdSearchDb.MdSearchCategoryID = mdSearchVm.MdSearchCategoryID;
                    _mdSearchService.Create(mdSearchDb);
                    _mdSearchService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, mdSearchDb);
                }
                catch (Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.BadGateway);
                }
                return response;
            });
        }

        [Route("updatemdsearch")]
        [HttpPost]
        public HttpResponseMessage UpdateModuleSearch(HttpRequestMessage request, MdSearchViewModel mdSearchVm)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                try
                {
                    var mdSearchDb = _mdSearchService.GetByID(mdSearchVm.ID);
                    mdSearchDb.BotID = mdSearchVm.BotID;
                    mdSearchDb.Title = mdSearchVm.Title;
                    mdSearchDb.Payload = mdSearchVm.Payload;
                    mdSearchDb.CardPayloadID = mdSearchVm.CardPayloadID;
                    mdSearchDb.TitlePayload = mdSearchVm.TitlePayload;
                    mdSearchDb.UrlAPI = mdSearchVm.UrlAPI;
                    mdSearchDb.MethodeAPI = mdSearchVm.MethodeAPI;
                    if (!String.IsNullOrEmpty(mdSearchVm.KeyCodeAPI) && !String.IsNullOrEmpty(mdSearchVm.KeyNameAPI))
                    {
                        mdSearchDb.KeyAPI = mdSearchVm.KeyNameAPI + ":" + mdSearchVm.KeyCodeAPI;
                    }

                    mdSearchDb.ParamAPI = mdSearchVm.ParamAPI;
                    mdSearchDb.MessageStart = mdSearchVm.MessageStart;
                    mdSearchDb.MessageError = mdSearchVm.MessageError;
                    mdSearchDb.MessageEnd = mdSearchDb.MessageEnd;
                    mdSearchDb.ID = mdSearchVm.ID;
                    mdSearchDb.MdSearchCategoryID = mdSearchVm.MdSearchCategoryID;
                    _mdSearchService.Update(mdSearchDb);
                    _mdSearchService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, mdSearchDb);
                }
                catch(Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.BadGateway);
                }              
                return response;
            });
        }

        #endregion
    }
}
