using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class FileDocumentViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Url { set; get; }

        public int Index { set; get; }

        public string TokenZalo { set; get; }

        public string TokenFacebook { set; get; }

        public string Extension { set; get; }

        public int? CardID { set; get; }

        public int? BotID { set; get; }
    }
}