using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class MdSearchViewModel
    {
        public int ID { set; get; }

        public string Title { set; get; }

        public string TitlePayload { set; get; }

        public string Payload { set; get; }

        public int? CardPayloadID { set; get; }

        public string UrlAPI { set; get; }

        public string KeyNameAPI { set; get; }

        public string KeyCodeAPI { set; get; }

        public string MethodeAPI { set; get; }

        public string ParamAPI { set; get; }

        public string MessageStart { set; get; }

        public string MessageError { set; get; }

        public string MessageEnd { set; get; }

        public int? ButtonModuleID { set; get; }

        public int? ModuleFollowCardID { set; get; }

        public int MdSearchCategoryID { set; get; }

        public int BotID { set; get; }
    }
}