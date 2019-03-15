using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
}