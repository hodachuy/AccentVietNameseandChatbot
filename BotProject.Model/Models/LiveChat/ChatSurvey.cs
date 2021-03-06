﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models.LiveChat
{
    [Table("ChatSurveys")]
    public class ChatSurvey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public bool IsShowName { get; set; }
        public string IsShowEmail { get; set; }
        public string IsShowPhone { get; set; }
        public long ChannelGroupID { get; set; }
    }
}
