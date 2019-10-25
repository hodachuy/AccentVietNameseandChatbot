using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common
{
    public class KendoRequest
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Sort> sort { get; set; }
        public Filter filter { get; set; }
    }

    public class Sort
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }

    public class Filter
    {
        public List<filters> filters { get; set; }
        public string Logic { get; set; }
    }

    public class filters
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
