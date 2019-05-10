using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("QuestionGroups")]
	public class QuestionGroup
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }

		public int Index { set; get; }

		public bool? IsKeyword { set; get; }

		public DateTime? CreatedDate { set; get; }

        public int BotID { set; get; }

		[Required]
		public int FormQuestionAnswerID { set; get; }

		[ForeignKey("FormQuestionAnswerID")]
		public virtual FormQuestionAnswer FormQuestionAnswer { set; get; }

        public virtual IEnumerable<Question> Questions { set; get; }

        public virtual IEnumerable<Answer> Answers { set; get; }
    }
}
