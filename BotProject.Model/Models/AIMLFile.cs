using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("AIMLFiles")]
    public class AIMLFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int? FormQnAnswerID { set; get; }

        public int? CardID { set; get; }

        public int BotID { set; get; }

        [MaxLength(50)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Src { set; get; }
        
        public string Extension { set; get; }

        public string Content { set; get; }

        public string UserID { set; get; }
    }
}
