using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ButtonLinks")]
    public class ButtonLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
       
        public string Type { set; get; }

        [MaxLength(50)]
        public string Title { set; get; }

        [MaxLength(256)]
        public string Url { set; get; }

        public string SizeHeight { set; get; }

        public int? TempGnrItemID { set; get; }

        public int? TempTxtID { set; get; }

        public int? CardID { set; get; }
    }
}
