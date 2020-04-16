using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Channels")]
    public class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get;}

        [Required]
        public long GroupChannelID { set; get; }

        [ForeignKey("GroupChannelID")]
        public virtual GroupChannel GroupChannel { set; get; }

        public string UserID { set; get; }
    }
}
