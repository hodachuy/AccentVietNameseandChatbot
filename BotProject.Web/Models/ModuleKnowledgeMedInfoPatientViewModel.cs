using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ModuleKnowledgeMedInfoPatientViewModel
    {
        public int ID { set; get; }

        public string Title { set; get; }

        public string OptionText { set; get; }

        public string Payload { set; get; }

        public int? CardPayloadID { set; get; }

        public string MessageEnd { set; get; }

        public int? ButtonModuleID { set; get; }

        public int BotID { set; get; }
    }
}