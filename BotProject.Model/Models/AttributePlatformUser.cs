using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("AttributePlatformUsers")]
    public class AttributePlatformUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string UserID { set; get; }
        public string AttributeKey { set; get; }
        public string AttributeValue { set; get; }
        public int BotID { set; get; }
        public string TypeDevice { set; get; }
    }
}
