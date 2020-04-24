using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("ActionChats")]
    public class ActionChat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [MaxLength(100)]
        public string Name { set; get; }
        public int Value { set; get; }
    }
}
