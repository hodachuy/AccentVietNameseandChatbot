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

        public string Title { set; get; }

        public string SubTitle { set; get; }

        public string Url { set; get; }

        public string Image { set; get; }

        public int? CardID { set; get; }

        public int? TempGnrGroupID { set; get; }

        [ForeignKey("TempGnrGroupID")]
        public virtual TemplateGenericGroup TemplateGenericGroups { set; get; }

        public virtual IEnumerable<ButtonPostback> ButtonPostbacks { set; get; }
        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }
    }
}
