using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    // pattern main TempSrai
    //<random>
    //<li>
    //  <srai>Tên parternText từ Card</srai>
    //</li>
    // 
    //<li>
    //  text abc
    //</li>
    //</random>

    [Table("Answers")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string ContentText { set; get; }

        public int? CardID { set; get; }

        public int QuestionGroupID { set; get; }

		//name: srai + postback + ContentCardID
		public string CardPayload { set; get; }

        public int? Index { set; get; }

        // random li nếu nội dung card select lấy patternText của Card thành nội dung srai

        //[ForeignKey("QuestionGroupID")]
        //public virtual QuestionGroup QuestionGroup { set; get; }
    }
}
