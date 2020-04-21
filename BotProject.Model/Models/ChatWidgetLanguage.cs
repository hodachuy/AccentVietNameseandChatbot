using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ChatWidgetLanguages")]
    public class ChatWidgetLanguage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string WelcomeMessage { set; get; }
        public int? WelcomeCardID { set; get; }
        public string TickedConfirmMessage { set; get; }
        public int BotID { set; get; }
    }
}
