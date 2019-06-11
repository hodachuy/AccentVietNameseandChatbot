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

namespace BotProject.Web.API
{
    [RoutePrefix("api/module")]
    public class ModuleController : ApiControllerBase
    {
        private IModuleService _moduleService;
        private IMdPhoneService _mdPhoneService;
        private IMdEmailService _mdEmailService;
        private IMdAgeService _mdAgeService;
        private IModuleKnowledegeService _mdKnowledegeService;
        public ModuleController(IErrorService errorService,
            IModuleService moduleService,
            IMdPhoneService mdPhoneService,
            IMdEmailService mdEmailService,
            IModuleKnowledegeService mdKnowledegeService,
            IMdAgeService mdAgeService) : base(errorService)
        {
            _moduleService = moduleService;
            _mdPhoneService = mdPhoneService;
            _mdEmailService = mdEmailService;
            _mdAgeService = mdAgeService;
            _mdKnowledegeService = mdKnowledegeService;
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
                    mdAge.MessageEnd = "Cảm ơn bạn, chúng đã tiếp nhận thông tin thành công!";
                    mdAge.ModuleID = moduleDb.ID;
                    //mdPhone.CardPayloadID = null;
                    //mdPhone.Payload = "";
                    mdAge.Title = "Xử lý tuổi";
                    mdAge.DictionaryKey = "age";
                    mdAge.DictionaryValue = "false";

                    _mdAgeService.Create(mdAge);
                    _mdAgeService.Save();
                }

                response = request.CreateResponse(HttpStatusCode.OK, moduleDb);
                return response;
            });
        }


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
        public HttpResponseMessage GetMdMedGetInfoPatient(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var module = _mdKnowledegeService.GetByMdMedInfoPatientID(id);
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
        #endregion
    }
}
