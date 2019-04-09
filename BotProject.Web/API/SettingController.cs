﻿using BotProject.Common;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
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
                    string url = PathServer.PathLogoSetting + fileName;
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