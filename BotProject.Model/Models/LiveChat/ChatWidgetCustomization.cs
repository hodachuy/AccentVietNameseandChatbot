using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("ChatWidgetCustomizations")]
    public class ChatWidgetCustomization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [MaxLength(50)]
        public string MinimizedWindow { set; get; }
        [MaxLength(50)]
        public string MaximizeWindow { set; get; }
        [MaxLength(50)]
        public string ThemeColor { set; get; }
        [MaxLength(50)]
        public string Position { set; get; }

        public string UrlLogo { set; get; }

        public int BotID { set; get; }
    }
}
