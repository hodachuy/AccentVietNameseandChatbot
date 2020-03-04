using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class SearchNlpQnAViewModel
    {
        public string _id { set; get; }
        public string question { set; get; }
        public string answer { set; get; }
        public string html { set; get; }
        public string field { set; get; }
        public int id { set; get; }
    }
    public class SearchSymptomViewModel
    {
        public string id { set; get; }
        public string name { set; get; }
        public string treatment { set; get; }
        public string description { set; get; }
        public string cause { set; get; }
        public string advice { set; get; }
    }
}
