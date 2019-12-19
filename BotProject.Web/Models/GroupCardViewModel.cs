﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class GroupCardViewModel
    {
        public int ID { set; get; }

        public int BotID { set; get; }

        public int Index { set; get; }

        public bool IsDelete { set; get; }

        public bool Status { set; get; }

        public string Name { set; get; }

        public virtual IEnumerable<CardViewModel> Cards { set; get; }
    }
}