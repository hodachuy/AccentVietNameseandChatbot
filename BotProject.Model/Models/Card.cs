using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Cards")]
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        //public Guid PatternText { set; get; }

        public string TemplateAIML { set; get; }

        public string TemplateHTML { set; get; }

        public string TemplateJSON { set; get; }

        public int BotID { set; get; }

        [ForeignKey("BotID")]
        public virtual Bot Bot { set; get; }

        public virtual IEnumerable<Button> Buttons { set; get; }
    }
}
