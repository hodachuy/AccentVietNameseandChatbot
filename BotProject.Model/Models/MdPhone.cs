using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("MdPhones")]
    public class MdPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Title { set; get; }
        public string Payload { set; get; }
        public int? CardPayloadID { set; get; }
        public string DictionaryKey { set; get; }
        public string DictionaryValue { set; get; }
        public string MessageStart { set; get; }
        public string MessageError { set; get; }
        public string MessageEnd { set; get; }
        public int ModuleID { set; get; }
        public int BotID { set; get; }

    }
}
