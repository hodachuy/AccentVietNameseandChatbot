using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class BotViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string UserID { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }

        public virtual IEnumerable<CardViewModel> Cards { set; get; }

        public virtual IEnumerable<QuestionGroupViewModel> QuestionGroups { set; get; }

        public virtual IEnumerable<AIMLViewModel> AIMLs { set; get; }
    }
}