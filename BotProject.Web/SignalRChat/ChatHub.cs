using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using BotProject.Service.Livechat;
using Microsoft.AspNet.Identity;
using BotProject.Model.Models.LiveChat;
using BotProject.Web.Infrastructure.Core;

namespace BotProject.Web.SignalRChat
{
    public class ChatHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        private string _connectionID;

        private const string CUSTOMER = "customer";
        private const string AGENT = "agent";

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
        public void ConnectChat(string customerId,string agentId, int channelGroupId, string threadId, string typeUserConnect)
        {
            string connectionId = Context.ConnectionId;
            try
            {
                if (typeUserConnect == CUSTOMER)
                {
                    // check customer exist
                    var customerDb = _customerService.GetById(customerId);
                    customerDb.ConnectionID = connectionId;
                    _customerService.Update(customerDb);
                    _customerService.Save();
                    // Kiểm tra customer tồn tại trong thread chat chưa
                    bool isExitsCustomerInThread = _chatCommonService.CheckCustomerInThreadParticipant(customerId);
                    if(isExitsCustomerInThread)
                    {
                        return;
                    }

                    // create thread chat
                    var threadDb = _chatCommonService.AddThreadMessage();
                    _chatCommonService.Save();

                    threadId = threadDb.ID.ToString();

                    // add to thread participant
                    ThreadParticipant threadParticipant = new ThreadParticipant();
                    threadParticipant.ChannelGroupID = channelGroupId;
                    threadParticipant.CreatedDate = DateTime.Now;
                    threadParticipant.ThreadID = threadDb.ID;
                    threadParticipant.UserID = agentId;
                    threadParticipant.CustomerID = customerId;
                    threadParticipant.CreatedDate = DateTime.Now;

                    _chatCommonService.AddThreadParticipant(threadParticipant);
                    _chatCommonService.Save();

                    // add hub group
                    _context.Groups.Add(connectionId, threadId);


                    // tín hiệu  thông báo tới agent khi có customer
                    Clients.Others.receiveNewThreadCustomer(channelGroupId, threadId, customerDb);

                    // gửi tín hiệu thông báo tới các userId trong Channel,
                    // có tín hiệu sẽ load danh sách customer bên trái kèm threadId,
                    // agent nào chat thì click vào kèm threadId và add tới groud chat
                }

                if (typeUserConnect == AGENT)
                {
                    var agent = _userManager.FindById(agentId);
                    agent.ConnectionID = connectionId;
                    _userManager.Update(agent);

                    _context.Groups.Add(connectionId, threadId);

                }
            }
            catch (Exception ex)
            {
                Clients.Caller.NoExistUser();
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
            string[] arrExcludeUserConnectionId = new string[] {Context.ConnectionId };
            _context.Clients.Group(threadId, arrExcludeUserConnectionId).getWriting(accountID, isStop);
        }

        /// <summary>
        ///  Gửi tin nhắn tới nhóm agent
        /// </summary>
        /// <param name="channelGroupId">Nhóm agent thuộc channelGroupId</param>
        /// <param name="message"></param>
        /// <param name="customerId"></param>
        /// <param name="threadId"></param>
        /// <param name="isBot"></param>
        public void SendMessageToGroupAgent(string channelGroupId, string message, string customerId, string threadId, bool isBot)
        {
            _context.Clients.Group(threadId).receiveMessagesCustomer(channelGroupId, customerId, message, threadId);
        }         

        /// <summary>
        /// Gửi tin nhắn tới customer
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="message"></param>
        public void SendMessageToCustomer(string agentId, string agentName, string message)
        {
            _context.Clients.All.receiveMessageAgent(agentId, agentName, message);
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
        public void GotoConnectionGroupThread(string threadId, string customerId, string agentId)
        {
            _connectionID = Context.ConnectionId;
            _context.Groups.Add(_connectionID, threadId);
            
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}