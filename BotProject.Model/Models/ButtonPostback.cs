﻿using System;
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

        public string DictionaryKey { set; get; }

        public string DictionaryValue { set; get; }

        public int Index { set; get; }

        public int? TempGnrItemID { set; get; }

        public int? TempTxtID { set; get; }

        public int? CardPayloadID { set; get; }

        public int? CardID { set; get; }

    }
}
