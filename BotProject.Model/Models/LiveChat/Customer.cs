using BotProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        public string ConnectionID { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public ApplicationChannel ApplicationChannels { set; get; }
        public long ChannelGroupID { set; get; }
        public int StatusChatValue { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime? LogoutDate { set; get; }
        public virtual IEnumerable<Device> Devices { set; get; }
    }
}
