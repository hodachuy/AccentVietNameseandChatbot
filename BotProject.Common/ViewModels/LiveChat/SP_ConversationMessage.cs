using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels.LiveChat
{
    public class SP_ConversationMessage
    {
        public long MessageID { set; get; }
        public DateTime Timestamp { set; get; }
        public string Body { set; get; }
        public long ConversationID { set; get; }
        public string AgentID { set; get; }
        public string CustomerID { set; get; }
        public bool IsBotChat { set; get; }
        public DateTime ConversationCreatedDate { set; get; }
        public long ThreadID { set; get; }
        public long ChannelGroupID {set;get;}
        public long Total { set; get; }
    }
}
