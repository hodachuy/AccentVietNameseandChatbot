using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("GroupChannels")]
    public class GroupChannel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<Channel> Channels { get; set; }
    }
}
