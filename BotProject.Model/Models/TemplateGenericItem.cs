using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("TemplateGenericItems")]
    public class TemplateGenericItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Type { set; get; }

        public string SubTitle { set; get; }

        public string Url { set; get; }

        public string Image { set; get; }

        public int TempGnrItemID { set; get; }

        [ForeignKey("TempGnrItemID")]
        public virtual TemplateGeneric TemplateGeneric { set; get; }

        public virtual IEnumerable<ButtonPostback> ButtonPostbacks { set; get; }
        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }
    }
}
