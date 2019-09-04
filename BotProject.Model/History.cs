using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model
{
    [Table("Histories")]
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string UserName { set; get; }

        public string UserSay { set; get; }

        public string BotHandle { set; get; }

        public string BotUnderStands { set; get; }

        public string MessageHistory { set; get; }

        public DateTime? CreatedDate { set; get; }

        public int? BotID { set; get; }
    }
}
