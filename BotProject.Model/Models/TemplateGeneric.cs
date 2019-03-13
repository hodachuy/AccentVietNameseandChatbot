using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("TemplateGenerics")]
    public class TemplateGeneric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Type { set; get; }

        public int CardID {set;get;}

        [ForeignKey("CardID")]
        public virtual Card Card { set; get; }

        public virtual IEnumerable<TemplateGenericItem> TemplateGenericItems { set; get; }

    }
}
