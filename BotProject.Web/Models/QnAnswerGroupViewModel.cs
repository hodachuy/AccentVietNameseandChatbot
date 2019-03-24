using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
	public class QnAnswerGroupViewModel
	{
		public int BotID { set; get; }
		public virtual IEnumerable<QuestionGroupViewModel> QuestionGroupViewModels { set; get; }
	}

	public class QuestionGroupViewModel
	{
		public int? ID { set; get; }
		public int BotQnAnswerID { set; get; }
		public int Index { set; get; }
		public bool IsKeyWord { set; get; }
		public virtual QnAViewModel QnAViewModel { set; get; }
	}

	public class QnAViewModel
	{
		public virtual IEnumerable<QuestionViewModel> QuestionViewModels { set; get; }
		public virtual IEnumerable<AnswerViewModel> AnswerViewModels { set; get; }
	}

	public class AnswerViewModel
	{
		public string ContentText { set; get; }
		public int? CardID { set; get; }
		public string CardPayload { set; get; }
		public int? Index { set; get; }
	}
	public class QuestionViewModel
	{
		public int? Index { set; get; }
		public string ContentText { set; get; }
		public bool IsThatStar { set; get; }
	}
}