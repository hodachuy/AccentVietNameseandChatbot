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

        public string Title { set; get; }

        public string Url { set; get; }

        public string SizeHeight { set; get; }

        public int TempGnrItemID { set; get; }

        public int TempTxtID { set; get; }

        [ForeignKey("TempGnrItemID")]
        public virtual TemplateGeneric TemplateGeneric { set; get; }

        [ForeignKey("TempTxtID")]
        public virtual TemplateText TemplateText { set; get; }
    }
}
