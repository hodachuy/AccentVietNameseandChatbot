using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("Messsages")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }
        public long ConversationID { set; get; }
        public DateTime Timestamp { set; get; }
        public string Body { set; get; }
        public string AgentID { set; get; }
        public string CustomerID { set; get; }
        public bool IsBotChat { set; get; }
    }
}
