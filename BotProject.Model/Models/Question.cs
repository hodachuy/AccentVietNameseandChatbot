using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Content { set; get; }

        public int Index { set; get; }

        public bool? IsThatStar { set; get; }

        //name: srai + postback + QuestionGroupID
        public string TempSrai { set; get; }

        public int QuestionGroupID { set; get; }

        [ForeignKey("QuestionGroupID")]
        public virtual QuestionGroup QuestionGroup { set; get; }
    }
}
