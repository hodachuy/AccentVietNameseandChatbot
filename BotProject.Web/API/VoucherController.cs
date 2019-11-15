using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Common.ViewModels;
using System.Web;
using System.Web.Script.Serialization;
using BotProject.Common;
using Newtonsoft.Json.Linq;
using BotProject.Model.Models;

namespace BotProject.Web.API
{
    [RoutePrefix("api/voucher")]
    public class VoucherController : ApiControllerBase
    {
        private IUserTelephoneService _userTelephoneService;
        public VoucherController(IErrorService errorService, IUserTelephoneService userTelephoneService) : base(errorService)
        {
            _userTelephoneService = userTelephoneService;
        }

        [Route("gettelephone")]
        [HttpPost]
        public HttpResponseMessage GetListTelephoneByVoucher(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var formRequest = HttpContext.Current.Request.Unvalidated.Form["requestFilter"];
                var requestFilter = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<KendoRequest>(formRequest);
                var botId = HttpContext.Current.Request.Unvalidated.Form["botId"];
                var botIdValue = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<string>(botId);

                int totalRow = 0;
                string filter = Common.CommonSer.ConvertFilertFormVoucherToString(requestFilter.filter);
                string sort = Common.CommonSer.ConvertSortToString(requestFilter.sort);
                int page = requestFilter.page;
                int pageSize = requestFilter.pageSize;

                if (!String.IsNullOrEmpty(filter))
                {
                    filter += " AND BotID = " + botIdValue;
                }
                else
                {
                    filter += "BotID = " + botIdValue;
                }

                filter += " AND IsDelete = 0";
                var lstTelephone = _userTelephoneService.GetUserTelephoneByVoucher(filter, "", page, pageSize, null).ToList();
                if (lstTelephone.Count() != 0)
                {
                    totalRow = lstTelephone[0].Total;
                }
                var paginationSet = new PaginationSet<StoreProcUserTelephoneByVoucherViewModel>()
                {
                    Items = lstTelephone,
                    Page = page,
                    TotalCount = totalRow,
                    MaxPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request, JObject jsonData)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response;
                dynamic json = jsonData;
                int usID = json.userTelephoneId;
                if (usID == 0)
                {
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                UserTelePhone us = new UserTelePhone();
                us = _userTelephoneService.GetById(usID);
                us.IsDelete = true;
                us.IsReceive = false;
                _userTelephoneService.Update(us);
                _userTelephoneService.Save();

                response = request.CreateResponse(HttpStatusCode.OK, true);
                return response;
            });
        }

    }
}
