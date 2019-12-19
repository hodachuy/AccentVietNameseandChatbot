using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
	public class FormQuestionAnswerViewModel
	{
		public int ID { set; get; }

		public string Name { set; get; }

		public string Alias { set; get; }

		public bool Status { set; get; }

		public int BotID { set; get; }

		public string UserID { set; get; }

        public string StrTempOtpCards { set; get; }

        public virtual IEnumerable<QuestionGroupViewModel> QuestionGroups { set; get; }
    }
}