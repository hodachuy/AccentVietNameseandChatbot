using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("FileDocuments")]
    public class FileDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Name { set; get; }

        public string Url { set; get; }

        public int Index { set; get; }

        public string TokenZalo { set; get; }

        public string TokenFacebook { set; get; }

        public string Extension { set; get; }

        public int? CardID { set; get; }

        public int? BotID { set; get; }
    }
}
