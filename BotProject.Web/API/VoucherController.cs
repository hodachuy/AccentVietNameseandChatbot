using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Common.ViewModels;

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
        [HttpGet]
        public HttpResponseMessage GetListTelephoneByVoucher(HttpRequestMessage request, int page, int pageSize, string botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                string filter = "BotID = " + botId;
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
    }
}
