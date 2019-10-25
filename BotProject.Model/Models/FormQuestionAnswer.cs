﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("FormQuestionAnswers")]
	public class FormQuestionAnswer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }

		public string Name { set; get; }

		public string Alias { set; get; }

		public bool Status { set; get; }

        public bool IsDelete { set; get; }

        public int BotID { set; get; }

		[ForeignKey("BotID")]
		public virtual Bot Bot { set; get; }

		public virtual IEnumerable<QuestionGroup> QuestionGroups { set; get; }

	}
}
