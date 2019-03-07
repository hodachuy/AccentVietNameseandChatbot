using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Content { set; get; }

        public int QuestionID { set; get; }

        public int CardID { set; get; }

        [ForeignKey("CardID")]
        public virtual Card Card { set; get; }

        [ForeignKey("QuestionID")]
        public virtual Question Question { set; get; }
    }
}
