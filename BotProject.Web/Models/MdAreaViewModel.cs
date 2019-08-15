using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class MdAreaViewModel
    {
        public int? ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public int? BotID { set; get; }
    }
}