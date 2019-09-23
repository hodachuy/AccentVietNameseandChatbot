using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class HistoryViewModel
    {
        public int ID { set; get; }

        public string UserName { set; get; }

        public string UserSay { set; get; }

        public string BotHandle { set; get; }

        public string BotUnderStands { set; get; }

        public string MessageHistory { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string StrCreatedDate { set; get; }

        public string Type { set; get; }

        public int? BotID { set; get; }
    }
}