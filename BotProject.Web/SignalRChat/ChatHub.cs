using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using BotProject.Service.Livechat;
using Microsoft.AspNet.Identity;
using BotProject.Model.Models.LiveChat;

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
        public ChatHub(ICustomerService customerService,
                       ApplicationUserManager userManager,
                       IChatCommonSerivce chatCommonService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _chatCommonService = chatCommonService;
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
                if(typeUserConnect == CUSTOMER)
                {
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

                    _chatCommonService.AddThreadParticipant(threadParticipant);

                    // update connectionId to customer
                    var customerDb = _customerService.GetById(customerId);
                    customerDb.ConnectionID = connectionId;
                    _customerService.Update(customerDb);
                    _customerService.Save();

                    // add hub group
                    _context.Groups.Add(connectionId, threadId);


                    // gửi tín hiệu thông báo tới các userId trong Channel,
                    // có tín hiệu sẽ load danh sách customer bên trái kèm threadId,
                    // agent nào chat thì click vào kèm threadId và add tới groud chat
                }
                if(typeUserConnect == AGENT)
                {

                }
            }
            catch (Exception ex)
            {
                Clients.Caller.NoExistUser();
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