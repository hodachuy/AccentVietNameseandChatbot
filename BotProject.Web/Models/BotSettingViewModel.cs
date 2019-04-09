﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class BotSettingViewModel
    {
        public int ID { set; get; }
        public string UserID { set; get; }

        public int BotID { set; get; }

        public string FormName { set; get; }

        public string Color { set; get; }

        public string Logo { set; get; }

        public int? CardID { set; get; }

        public int TextIntroductory { set; get; }

        public bool? IsActiveIntroductory { set; get; }

        public bool? IsMDSearch { set; get; }
    }
}