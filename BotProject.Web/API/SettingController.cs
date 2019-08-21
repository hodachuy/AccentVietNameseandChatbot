using BotProject.Common;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BotProject.Web.API
{
    [RoutePrefix("api/setting")]
    public class SettingController : ApiControllerBase
    {
        private ISettingService _settingService;
        public SettingController(IErrorService errorService,
                                ISettingService settingService) : base(errorService)
        {
            _settingService = settingService;
        }

        [Route("getbybotid")]
        [HttpGet]
        public HttpResponseMessage GetByBotId(HttpRequestMessage request, int botId)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                var setting = _settingService.GetSettingByBotID(botId);
                response = request.CreateResponse(HttpStatusCode.OK, setting);
                return response;
            });
        }

        [Route("createupdate")]
        [HttpPost]
        public HttpResponseMessage CreateUpdate(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                bool result = true;
                var botSetting = System.Web.HttpContext.Current.Request.Unvalidated.Form["bot-setting"];
                if(botSetting == null)
                {
                    result = false;
                    response = request.CreateResponse(HttpStatusCode.NoContent, result);
                    return response;
                }
                var botSettingVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<BotSettingViewModel>(botSetting);

                var botSystem = System.Web.HttpContext.Current.Request.Unvalidated.Form["bot-systemconfig"];
                if (botSystem == null)
                {
                    result = false;
                    response = request.CreateResponse(HttpStatusCode.NoContent, result);
                    return response;
                }
                var botSystemVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<List<SystemConfig>>(botSystem);

                Setting settingDb = new Setting();
                settingDb.UpdateSetting(botSettingVm);
                _settingService.Update(settingDb);
                _settingService.Save();

                if(botSystemVm.Count() != 0)
                {
                    _settingService.DeleteConfigByBotID(settingDb.BotID);
                    foreach (var item in botSystemVm)
                    {
                        SystemConfig sys = new SystemConfig();
                        sys.BotID = item.BotID;
                        sys.Code = item.Code;
                        sys.ValueString = item.ValueString;
                        sys.ValueInt = item.ValueInt;
                        _settingService.CreateKeyConfig(sys);
                    }
                    _settingService.Save();
                }

                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("importlogo")]
        [HttpPost]
        public HttpResponseMessage ImportLogo(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                var form = HttpContext.Current.Request.Unvalidated.Form["botId"];
                var botId = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<int>(form);
                var file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    Guid id = Guid.NewGuid();
                    string extentsion = new FileInfo(file.FileName).Extension.ToLower();
                    string fileName = "logo-bot-"+ botId+ "-"+id + "-" + new FileInfo(file.FileName).Name;

                    file.SaveAs(Path.Combine(PathServer.PathLogoSetting + fileName));
                    string url = "assets/images/logo/" + fileName;
                    response = request.CreateResponse(HttpStatusCode.OK, url);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Implement");
                }

                return response;
            });
        }
    }
}
