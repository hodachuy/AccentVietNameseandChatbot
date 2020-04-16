using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ThreadParticipants")]
    public class ThreadParticipant
    {
        [Column(Order = 1)]
        public long ThreadID { set; get; }

        [Column(Order = 2)]
        public long GroupChannelID { set; get; }

        public string UserID { set; get; }
        public string CustomerID { set; get; }

        [ForeignKey("GroupChannelID")]
        public virtual GroupChannel GroupChannel { set; get; }

        [ForeignKey("ThreadID")]
        public virtual Thread Thread { set; get; }
    }
}
