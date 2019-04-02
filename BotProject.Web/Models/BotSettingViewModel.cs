using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class BotSettingViewModel
    {
        public string UserID { set; get; }
        public int BotID { set; get; }
        public string BotName { set; get; }
        public string Color { set; get; }
        public string Logo { set; get; }

    }
}