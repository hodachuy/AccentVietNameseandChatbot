using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotProject.Model.Models
{
    [Table("MdQuestions")]
    public class MdQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Title { set; get; }

        public string ContentText { set; get; }

        public string ContentHTML { set; get; }

        public bool IsTrained { set; get; }

        public bool IsDelete { set; get; }

        public DateTime? CreatedDate { set; get; }

        public int? AreaID { set; get; }
    }
}
