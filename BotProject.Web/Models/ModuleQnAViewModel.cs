using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ModuleQnAViewModel
    {
        public int? QuesID { set; get; }

        public int? AnsID { set; get; }

        public string QuesContent { set; get; }

        public string AnsContent { set; get; }

        public string AreaName { set; get; }

        public int? AreaID { set; get; }

    }
}