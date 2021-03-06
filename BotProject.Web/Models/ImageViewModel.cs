﻿using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ImageViewModel
    {
        public int ID { set; get; }

        public string Url { set; get; }

        public int Index { set; get; }

        public int? CardID { set; get; }

        public int? BotID { set; get; }
    }
}