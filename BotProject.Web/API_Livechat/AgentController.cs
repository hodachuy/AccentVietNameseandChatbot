﻿using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using Newtonsoft.Json.Linq;
using BotProject.Service.Livechat;

namespace BotProject.Web.API_Livechat
{
    [RoutePrefix("api/lc_agent")]
    public class AgentController : ApiControllerBase
    {
        private IChannelService _channelService;
        public AgentController(IErrorService errorService,
                               IChannelService channelService) : base(errorService)
        {
            _channelService = channelService;
        }

        [Route("getListAgentOfChannel")]
        [HttpPost]
        public HttpResponseMessage GetListAgentOfChannelByGroupChannel(HttpRequestMessage request, JObject jsonData)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response;
                dynamic json = jsonData;
                int channelGroupID = json.channelGroupId;
                var lstAgent = _channelService.GetListChannelByChannelGroupID(channelGroupID).ToList();
                response = request.CreateResponse(HttpStatusCode.OK, lstAgent);
                return response;
            });
        }

		[Route("getListAgentOnline")]
		[HttpPost]
		public HttpResponseMessage GetListAgentOnlineOfChannelByGroupChannel(HttpRequestMessage request, JObject jsonData)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response;
				dynamic json = jsonData;
				int channelGroupID = json.channelGroupId;
				var lstAgent = _channelService.GetListChannelByChannelGroupID(channelGroupID).Where(x=>x.StatusChatValue == 200).ToList();
				response = request.CreateResponse(HttpStatusCode.OK, lstAgent);
				return response;
			});
		}
	}
}
