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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }

        public long ThreadID { set; get; }

        public long ChannelGroupID { set; get; }

        public string UserID { set; get; }

        public string CustomerID { set; get; }

        public DateTime CreatedDate { set; get; }

        [ForeignKey("ChannelGroupID")]
        public virtual ChannelGroup GroupChannel { set; get; }

        [ForeignKey("ThreadID")]
        public virtual Thread Thread { set; get; }
    }
}
