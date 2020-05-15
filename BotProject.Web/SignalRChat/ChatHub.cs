using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using BotProject.Service.Livechat;
using Microsoft.AspNet.Identity;

namespace BotProject.Web.SignalRChat
{
    public class ChatHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        private string _connectionID;

        private ICustomerService _customerService;
        private ApplicationUserManager _userManager;
        public ChatHub(ICustomerService customerService,
                       ApplicationUserManager userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        /// <summary>
        /// Khách hàng kết nối chat tới tổng đài viên
        /// agentId gửi kèm theo trong static/app.js
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="threadId"></param>
        /// <param name="threadId"></param>
        public void ConnectChat(string customerId,string agentId, string channelGroupId, string threadId, string typeConnect)
        {
            var connectionId = Context.ConnectionId;
            _context.Groups.Add(connectionId, threadId);
            string connectInfo = "";
            switch (typeConnect)
            {
                // gửi tín hiệu sếp vào hàng đợi, chờ xác nhận để chat
                case "CONNECT_QUEUE":
                    connectInfo = "Connnect Waitting";
                    Clients.Caller.onConnected(connectInfo, customerId);
                    // gửi thread đăng ký tham gia vào vòng đợi tới agent
                    Clients.Group(threadId).addCustomerIntoQueue(agentId, threadId);
                    break;
                // gửi tín hiệu vào chat thẳng
                case "CONNECT_GOTO":             
                    connectInfo = "Connect Success";
                    Clients.Caller.onConnected(connectInfo, customerId);
                    Clients.Group(threadId).addCustomerGotoChat(agentId, threadId);
                    break;
                // gửi tín hiệu kết nối từ agent này tới agent khác hỗ trợ
                case "CONNECT_SUPPORT":
                    connectInfo = "Connect Support";
                    string agentSupportConnectionId = Context.ConnectionId;
                    Clients.Client(agentSupportConnectionId).onConnected(connectInfo, agentId);
                    break;
            }
        }
        public void GetTyping(string threadId, string accountID, bool isStop)
        {
            string[] arrExcludeUserConnectionId = new string[] {Context.ConnectionId };
            _context.Clients.Group(threadId, arrExcludeUserConnectionId).getWriting(accountID, isStop);
        }

        public void SendMessageAgent(string accountId, string message, string userName, string threadId, bool isAccept)
        {
            if (isAccept)
            {
                _context.Clients.Group(threadId).getMessages(accountId, userName, message, threadId);
            }
        }         

        public void SendMessageCustomer(string accountId, string message)
        {
            _context.Clients.All.receiveMessageAgent(accountId, message);
        }

        public void OnchangeStatusAgent(string agentId, int status)
        {

        }

        public void OnchangeStatusCustomer(string customerId, int status)
        {

        }

        /// <summary>
        /// Kết nối tới nhóm chat
        /// </summary>
        /// <param name="channelGroupId"></param>
        /// <param name="customerId"></param>
        /// <param name="agentId"></param>
        public void GotoConnectionChannelGroup(string channelGroupId, string customerId, string agentId)
        {
            _connectionID = Context.ConnectionId;
            _context.Groups.Add(_connectionID, channelGroupId);
            
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}