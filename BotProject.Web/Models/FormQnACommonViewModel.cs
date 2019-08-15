using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
	public class FormQnACommonViewModel
	{
		public int BotID { set; get; }
        public int FormQuestionAnswerID { set; get; }
        //Add - Update
        public string TypeAction { set; get; }
		public virtual IEnumerable<QuestionGroupViewModel> QuestionGroupViewModels { set; get; }
	}

	public class QuestionGroupViewModel
	{
		public int ID { set; get; }
		public int FormQuestionAnswerID { set; get; }
		public int Index { set; get; }
		public bool IsKeyWord { set; get; }
        public int BotID { set; get; }
		public virtual QnAViewModel QnAViewModel { set; get; }
	}

	public class QnAViewModel
	{
		public virtual IEnumerable<QuestionViewModel> QuestionViewModels { set; get; }
		public virtual IEnumerable<AnswerViewModel> AnswerViewModels { set; get; }
	}

	public class AnswerViewModel
	{
        public int ID { set; get; }
        public string ContentText { set; get; }
		public int? CardID { set; get; }
		public string CardPayload { set; get; }
		public int? Index { set; get; }
		public int QuestionGroupID { set; get; }
	}
	public class QuestionViewModel
	{
        public int ID { set; get; }
		public int? Index { set; get; }
        public string CodeSymbol { set; get; }
		public string ContentText { set; get; }
		public bool IsThatStar { set; get; }
		public int QuestionGroupID { set; get; }
        public string Target { set; get; }
        public bool IsSendAPI { set; get; }
	}
}