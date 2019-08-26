using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotProject.Model.Models
{
    [Table("MdAnswers")]
    public class MdAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get;}

        public string ContentText { set; get; }

        public string ContentHTML { set; get; }

        public bool IsDelete { set; get; }

        public DateTime? CreatedDate { set; get; }

        public int? MQuestionID { set; get; }

        public int? BotID { set; get; }
    }
}
