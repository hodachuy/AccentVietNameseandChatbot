using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ButtonPostbacks")]
    public class ButtonPostback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Type { set; get; }

        public string Payload { set; get; }

        public string Title { set; get; }

        public int? TempGnrID { set; get; }

        public int? TempTxtID { set; get; }

        public int? CardPayloadID { set; get; }

        public int? CardID { set; get; }

    }
}
