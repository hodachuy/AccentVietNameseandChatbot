using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("ThreadParticipants")]
    public class ThreadParticipant
    {
        [Key]
        [Column(Order = 1)]
        public long ThreadID { set; get; }

        [Key]
        [Column(Order = 2)]
        public long ChannelGroupID { set; get; }

        public string CustomerID { set; get; }

        public DateTime CreatedDate { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get;}

        [ForeignKey("ChannelGroupID")]
        public virtual ChannelGroup GroupChannel { set; get; }

        [ForeignKey("ThreadID")]
        public virtual Thread Thread { set; get; }
    }
}
