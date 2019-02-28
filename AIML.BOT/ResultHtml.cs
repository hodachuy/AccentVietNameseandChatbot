using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIML.BOT
{
    public class ResultHtml
    {
        public string Message { get; set; }
        public List<string> TextPostback { get; set; }
        public int TotalPostback { get; set; }
    }
    public class TagHtml
    {
        public string Body { get; set; }
        public string ButtonPostback { get; set; }
    }
    
}
