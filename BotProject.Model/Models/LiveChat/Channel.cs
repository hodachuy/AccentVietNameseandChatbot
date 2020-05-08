using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("Channels")]
    public class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get;}

        [Required]
        public long ChannelGroupID { set; get; }

        [ForeignKey("ChannelGroupID")]
        public virtual ChannelGroup ChannelGroup { set; get; }

        public string UserID { set; get; }
    }
}
