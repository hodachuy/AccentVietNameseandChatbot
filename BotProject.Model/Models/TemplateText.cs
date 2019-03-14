using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("TemplateTexts")]
    public class TemplateText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Type { set; get; }

        public string Text { set; get; }

        public int? CardID { set; get; }

        public virtual IEnumerable<ButtonPostback> ButtonPostbacks { set; get; }
        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }
    }
}
