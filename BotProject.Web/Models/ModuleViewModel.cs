using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ModuleViewModel
    {
        public int ID { set; get; }

        public string Title { set; get; }

        public string Text { set; get; }

        public string Type { set; get; }

        public string Payload { set; get; } // click postback_module_checkphone, neu co module phai them key value vao predicate value phone = false
        public int BotID { set; get; }
    }
}