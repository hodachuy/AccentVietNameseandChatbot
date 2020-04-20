using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BotProject.Web.SignalRChat
{
    public class ChatHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        /// <summary>
        /// Khách hàng kết nối chat tới tổng đài viên
        /// agentId gửi kèm theo trong static/app.js
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="threadId"></param>
        public void ConnectChat(string customerId, string agentId, string groupChanelId, string threadId, string typeConnect)
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
        public void SendMessage(string accountId, string message,string userName, string threadId, bool isAccept)
        {
            if (isAccept)
            {
                _context.Clients.Group(threadId).getMessages(accountId, userName, message, threadId);
            }
        }

        public void OnchangeStatus()
        {

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}