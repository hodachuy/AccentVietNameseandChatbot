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
        public long GroupChannelID { set; get; }

        public string UserID { set; get; }

        public string CustomerID { set; get; }

        public DateTime CreatedDate { set; get; }

        [ForeignKey("GroupChannelID")]
        public virtual GroupChannel GroupChannel { set; get; }

        [ForeignKey("ThreadID")]
        public virtual Thread Thread { set; get; }
    }
}
