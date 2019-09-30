using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class BotSettingViewModel
    {
        public int ID { set; get; }
        public string UserID { set; get; }

        public int BotID { set; get; }

        public string FormName { set; get; }

        public string Color { set; get; }

        public string Logo { set; get; }

        public int? CardID { set; get; }

        public bool IsProactiveMessage { set; get; }

        public int Timeout { set; get; }

        public string ProactiveMessageText { set; get; }

        public string FacebookPageToken { set; get; }

        public string FacebookAppSecrect { set; get; }

        public string ZaloPageToken { set; get; }

        public string ZaloAppSecrect { set; get; }

        public string ZaloQRCode { set; get; }

        public string TextIntroductory { set; get; }

        public bool IsActiveIntroductory { set; get; }

        public bool IsMDSearch { set; get; }        
        
        public string StopWord { set; get; }
    }
}