using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Service.Livechat;
using System.Web.Script.Serialization;
using BotProject.Web.Models.Livechat;
using BotProject.Model.Models.LiveChat;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Common;

namespace BotProject.Web.API_Livechat
{
    [RoutePrefix("api/lc_customer")]
    public class CustomerController : ApiControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(IErrorService errorService, ICustomerService customerService) : base(errorService)
        {
            _customerService = customerService;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                try
                {
                    var customer = System.Web.HttpContext.Current.Request.Unvalidated.Form["customer"];
                    if (customer == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.NoContent, false);
                        return response;
                    }
                    var customerVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<CustomerViewModel>(customer);
                    var device = System.Web.HttpContext.Current.Request.Unvalidated.Form["device"];
                    if (device == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.NoContent, false);
                        return response;
                    }
                    var deviceVm = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<DeviceViewModel>(device);

                    Customer customerDb = new Customer();
                    customerDb.UpdateCustomer(customerVm);
                    customerDb.Name = deviceVm.IPAddress;
                    customerDb.StatusChatValue = CommonConstants.USER_ONLINE;
                    _customerService.Create(customerDb);
                    _customerService.Save();

                    Device deviceDb = new Device();
                    deviceDb.UpdateDevice(deviceVm);
                    deviceDb.CustomerID = customerVm.ID;
                    _customerService.CreateDevice(deviceDb);

                    _customerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, true);
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.OK, false);
                }

                return response;
            });
        }

        [Route("getAll")]
        [HttpGet]
        public HttpResponseMessage GetListCustomer(HttpRequestMessage request, int channelGroupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lstCustomer = _customerService.GetListCustomerByChannelGroupId(channelGroupId);           
                response = request.CreateResponse(HttpStatusCode.OK, lstCustomer);
                return response;
            });
        }

        [Route("getById")]
        [HttpGet]
        public HttpResponseMessage GetCustomerByID(HttpRequestMessage request, string customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var customerDb = _customerService.GetById(customerId);
                response = request.CreateResponse(HttpStatusCode.OK, customerDb);
                return response;
            });
        }

        [Route("getDeviceByCustomerId")]
        [HttpGet]
        public HttpResponseMessage GetDeviceByCustomerID(HttpRequestMessage request, string customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var deviceDb = _customerService.GetDeviceByCustomerId(customerId);
                response = request.CreateResponse(HttpStatusCode.OK, deviceDb);
                return response;
            });
        }
    }
}
