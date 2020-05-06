using BotProject.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Bots")]
    public class Bot : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

		[MaxLength(256)]
		public string Alias { set; get; }

        public bool IsTemplate { set; get; }

        public string ImageTemplate { set; get; }

        public int? BotCloneParentID { set; get; }

        //public DateTime? LastTrainTime { set; get; }

        [Required]
        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserID { set; get; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { set; get; }

        public virtual IEnumerable<Card> Cards { set; get; }

        public virtual IEnumerable<QuestionGroup> QuestionGroups { set; get; }

        public virtual IEnumerable<AIMLFile> AIMLFiles { set; get; }

		public virtual IEnumerable<FormQuestionAnswer> FormQuestionAnswers { set; get; }
	}
}
