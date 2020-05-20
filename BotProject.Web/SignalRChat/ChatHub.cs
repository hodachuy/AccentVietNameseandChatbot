using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using BotProject.Service.Livechat;
using Microsoft.AspNet.Identity;
using BotProject.Model.Models.LiveChat;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Log4Net;
using System.Linq;
using BotProject.Common;

namespace BotProject.Web.SignalRChat
{
    public class ChatHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        private const string USER_CUSTOMER = "customer";
        private const string USER_AGENT = "agent";
        private const string USER_BOT = "bot";

        private ICustomerService _customerService;
        private IChatCommonSerivce _chatCommonService;
        private ApplicationUserManager _userManager;
        public ChatHub()
        {
            _customerService = ServiceFactory.Get<ICustomerService>();
            _userManager = ServiceFactory.Get<ApplicationUserManager>();
            _chatCommonService = ServiceFactory.Get<IChatCommonSerivce>();
        }

        /// <summary>
        /// Khách hàng kết nối chat tới tổng đài viên
        /// agentId gửi kèm theo trong static/app.js
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="threadId"></param>
        /// <param name="threadId"></param>
        public void ConnectCustomerToChannelChat(string customerId, long channelGroupId)
        {
            string connectionId = Context.ConnectionId;
            try
            {
                // check customer exist
                var customerDb = _customerService.GetById(customerId);
                customerDb.ConnectionID = connectionId;
                customerDb.StatusChatValue = CommonConstants.USER_ONLINE;
                _customerService.Update(customerDb);
                _customerService.Save();
                // Kiểm tra customer tồn tại trong thread chat chưa
                bool isExitsCustomerInThread = _chatCommonService.CheckCustomerInThreadParticipant(customerId);
                if (isExitsCustomerInThread)
                {
                    OnchangeStatusCustomerOnline(channelGroupId, customerDb.ID, connectionId);
                    return;
                }

                // create thread chat
                var threadDb = _chatCommonService.AddThreadMessage();
                _chatCommonService.Save();

                // add to thread participant
                ThreadParticipant threadParticipant = new ThreadParticipant();
                threadParticipant.ChannelGroupID = channelGroupId;
                threadParticipant.CreatedDate = DateTime.Now;
                threadParticipant.ThreadID = threadDb.ID;
                threadParticipant.CustomerID = customerId;
                threadParticipant.CreatedDate = DateTime.Now;

                _chatCommonService.AddThreadParticipant(threadParticipant);
                _chatCommonService.Save();

                // add hub group
                _context.Groups.Add(connectionId, threadDb.ID.ToString());

                // tín hiệu thông báo tới agent khi có customer
                Clients.Others.receiveNewThreadCustomer(channelGroupId, threadDb.ID, customerDb);

                // gửi tín hiệu thông báo tới các userId trong Channel,
                // có tín hiệu sẽ load danh sách customer bên trái kèm threadId,
                // agent nào chat thì click vào kèm threadId và add tới groud chat
            }
            catch (Exception ex)
            {
                BotLog.Error(ex.StackTrace + " " + ex.Source + " " + ex.Message);
                Clients.Caller.showErrorMessage("Error");
            }
        }

        /// <summary>
        /// Tín hiệu đang gõ tin nhắn
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="accountID"></param>
        /// <param name="isStop"></param>
        public void GetTyping(string threadId, string accountID, bool isStop)
        {
            string[] arrExcludeUserConnectionId = new string[] { Context.ConnectionId };
            _context.Clients.Group(threadId, arrExcludeUserConnectionId).getWriting(accountID, isStop);
        }

        public void ConnectAgentToListCustomer(long channelGroupId)
        {
            string connectionId = Context.ConnectionId;
            // kết nối tới channelGroup
            ConnectAgentToChannelGroup(connectionId, channelGroupId);

            string filter = "tp.ChannelGroupID = " + channelGroupId + " and c.StatusChatValue = 200";
            var lstCustomerOnline = _chatCommonService.GetCustomerJoinChatByChannelGroupID(filter, "", 1, Int32.MaxValue, null);
            if (lstCustomerOnline != null && lstCustomerOnline.Count() != 0)
            {
                foreach(var customer in lstCustomerOnline)
                {
                    // kết nối tới từng customer
                    _context.Groups.Add(connectionId, customer.ThreadID.ToString());
                }
            }
        }

        public void ConnectAgentToSingleCustomer(string threadId)
        {
            _context.Groups.Add(Context.ConnectionId, threadId);
        }

        public void ConnectAgentToChannelGroup(string connectionId, long channelGroupId)
        {
            _context.Groups.Add(connectionId, channelGroupId.ToString());
        }

        /// <summary>
        /// Gửi tin nhắn tới user tham gia thread (agent và customer)
        /// </summary>
        /// <param name="channelGroupId"></param>
        /// <param name="threadId"></param>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="isBot"></param>
        /// <param name="typeUserRecievedMessage">customer or agent</param>
        public void SendMessageToGroupJoinThread(string channelGroupId, string threadId, string message, string userId, string typeUserRecievedMessage)
        {
            _context.Clients.Group(threadId).receiveMessages(channelGroupId, threadId, userId, message, typeUserRecievedMessage);
        }

        //public void OnchangeStatusAgent(long channelGroupId, string agentId, int status)
        //{
        //    var agentDb = _userManager.FindById(agentId);
        //    agentDb.StatusChatValue = status;
        //    _userManager.Update(agentDb);
        //    //send to client
        //    _context.Clients.All.getStatusAgent(channelGroupId, agentId, status);
        //}

        public void OnchangeStatusCustomerOffline(string connectionId)
        {
            var customerDb = _customerService.GetCustomerByConnectionId(connectionId);
            if (customerDb != null)
            {
                customerDb.ConnectionID = "";
                customerDb.StatusChatValue = CommonConstants.USER_OFFLINE;
                customerDb.LogoutDate = DateTime.Now;
                _customerService.Update(customerDb);
                _customerService.Save();
                _context.Clients.Group(customerDb.ChannelGroupID.ToString(), customerDb.ConnectionID).getStatusCustomerOffline(customerDb.ID, CommonConstants.USER_OFFLINE);
            }
        }
        public void OnchangeStatusCustomerOnline(long ChannelGroupID,string customerId, string connectionId)
        {
            _context.Clients.Group(ChannelGroupID.ToString(), connectionId).getStatusCustomerOnline(customerId, CommonConstants.USER_ONLINE);
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                // customer
                OnchangeStatusCustomerOffline(Context.ConnectionId);

                Console.WriteLine(String.Format("Client {0} explicitly closed the connection.", Context.ConnectionId));
            }
            else
            {
                Console.WriteLine(String.Format("Client {0} timed out .", Context.ConnectionId));
            }


            return base.OnDisconnected(stopCalled);
        }

        public void DisconnectedCustomer()
        {
            OnchangeStatusCustomerOffline(Context.ConnectionId);
        }
    }
}