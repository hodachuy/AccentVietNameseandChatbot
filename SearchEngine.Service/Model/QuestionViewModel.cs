using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Service.Model
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string AutoComplete { get; set; }
        public int Total { get; set; }
    }
}
