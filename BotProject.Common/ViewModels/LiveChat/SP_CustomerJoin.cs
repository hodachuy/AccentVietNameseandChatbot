using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels.LiveChat
{
    public class SP_CustomerJoin
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public long ThreadID { get; set; }
        public long ChannelGroupID { get; set; }
        public int StatusChatValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ConnectedDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public int ApplicationChannels { get; set; }
        public string TagName { get; set; }
        public int Total { set; get; }
    }
}
