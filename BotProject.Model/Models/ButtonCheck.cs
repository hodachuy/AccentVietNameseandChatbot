using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ButtonChecks")]
    public class ButtonCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Title { set; get; }

        public string TypeButton { set; get; }

        public int Index { set; get; }

        public string Payload { set; get; }// dictionary var [payload_checkbox_ID]

        public string DictionaryKey { set; get; }

        public string DictionaryValue { set; get; }

        public int? TempTxtID { set; get; }

        public int CardID { set; get; }

        public bool IsCheck { set; get; }


    }
}
