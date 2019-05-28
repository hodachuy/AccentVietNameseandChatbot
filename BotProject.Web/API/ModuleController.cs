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
        public ModuleController(IErrorService errorService, IModuleService moduleService,
            IMdPhoneService mdPhoneService) : base(errorService)
        {
            _moduleService = moduleService;
            _mdPhoneService = mdPhoneService;
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
                    mdPhone.MessageStart = "Vui lòng nhấn vào số điện thoại (nếu có) hoặc nhập số điện thoại của bạn!";
                    mdPhone.MessageError = "Bạn đã nhập sai định dạng, vui lòng nhập lại!";
                    mdPhone.MessageEnd = "Chúng tôi đã nhận được số điện thoại của bạn. Cảm ơn!";
                    mdPhone.ModuleID = moduleDb.ID;
                    mdPhone.CardPayloadID = null;
                    mdPhone.Payload = "";
                    mdPhone.Title = "Xử lý số điện thoại";
                    mdPhone.DictionaryKey = "phone";
                    mdPhone.DictionaryValue = "false";

                    _mdPhoneService.Create(mdPhone);
                    _mdPhoneService.Save();
                }

                response = request.CreateResponse(HttpStatusCode.OK, moduleDb);
                return response;
            });
        }
    }
}
