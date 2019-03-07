using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Buttons")]
    public class Button
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Type { set; get; }

        public string PostbackCard { set; get; }

        public string Title { set; get; }

        public string Url { set; get; }

        public int CardID { set; get; }

        [ForeignKey("CardID")]
        public virtual Card Card { set; get; }
    }
}
