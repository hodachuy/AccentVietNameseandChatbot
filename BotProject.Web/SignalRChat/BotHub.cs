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
        public BotHub()
        {

        }
        public static void SendMessageBot(string conenectionID, string userId, string text)
        {
            _context.Clients.AllExcept(conenectionID).receiveMessageBot(userId, text);
        }
    }
}