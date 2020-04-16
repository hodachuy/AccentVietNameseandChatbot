using BotProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public string ID { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; }
        public string Avatar { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        public bool Gender { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public ApplicationChannel ApplicationChannels { set; get; }
        public long ChannelChatGroupId { set; get; }
        public virtual Device Device { set; get; }
        public int StatusActivityValue { set; get; }
        public int ActionChatValue { set; get; }
    }
}
