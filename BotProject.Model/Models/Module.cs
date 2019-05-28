using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Modules")]
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(150)]
        public string Title { set; get; }

        [StringLength(150)]
        public string Text { set; get; }// Text = phone when append buttonModule sẽ là postback_module_phone

        public string Payload { set; get; }// click postback_module_phone(ten nay se xuất hiện), neu co module phai them key value vao predicate value phone = false
    
        public string Type { set; get; }// handle, knowledge
      
        [Required]
        public int BotID { set; get; }
    }
}
