using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.SignalRChat
{
    public class BotHub : Hub
    {
        static IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
    }
}