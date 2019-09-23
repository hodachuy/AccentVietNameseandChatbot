using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class HandleResultBotViewModel
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public string Postback { set; get; }
        public string ResultAPI { set; get; }
        public string TemplateJsonFacebook { set; get; }
        public string TemplateJsonZalo { set; get; }
    }
}
