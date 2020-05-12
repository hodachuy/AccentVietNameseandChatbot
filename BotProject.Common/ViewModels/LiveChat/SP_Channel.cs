using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels.LiveChat
{
    public class SP_Channel
    {
        public long ChannelGroupID { get; set; }
        public string ChannelGroupName { get; set; }
        public long ChannelID { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public int ApplicationGroupID { get; set; }
        public string ApplicationGroupName { get; set; }
    }
}
