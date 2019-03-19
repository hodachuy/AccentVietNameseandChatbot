using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("FileCards")]
    public class FileCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Url { set; get; }

        public string Name { set; get; }

        public string Type { set; get; }

        public int? CardID { set; get; }

        public int? BotID { set; get; }
    }
}
