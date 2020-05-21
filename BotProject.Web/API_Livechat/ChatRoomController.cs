using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Service.Livechat;
using BotProject.Common;

namespace BotProject.Web.API_Livechat
{
    [RoutePrefix("api/lc_chatroom")]
    public class ChatRoomController : ApiControllerBase
    {
        private IChatCommonSerivce _chatCommonService;
        public ChatRoomController(IErrorService errorService, IChatCommonSerivce chatCommonService) : base(errorService)
        {
            _chatCommonService = chatCommonService;
        }

        [Route("getListCustomerJoinChatChannel")]
        [HttpGet]
        public HttpResponseMessage GetListCustomerJoinChannelChat(HttpRequestMessage request, int channelGroupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string filter = "tp.ChannelGroupID = " + channelGroupId;
                var lstCustomerJoinChat = _chatCommonService.GetCustomerJoinChatByChannelGroupID(filter, "", 1, 20, null)
                                          .OrderByDescending(x=>x.StatusChatValue == CommonConstants.USER_ONLINE);
                response = request.CreateResponse(HttpStatusCode.OK, lstCustomerJoinChat);
                return response;
            });
        }
    }
}
