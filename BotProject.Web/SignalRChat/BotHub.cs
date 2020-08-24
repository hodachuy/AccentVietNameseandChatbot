using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.SignalRChat
{
    public class BotHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<BotHub>();
        public void SendMessageBot(string userId, string text)
        {
            //_context.Clients.AllExcept(conenectionID).receiveMessageBot(userId, text);
			// Gửi lại thread ra customer
			Clients.Caller.receiveMessageBot(userId, text);
		}
    }
}