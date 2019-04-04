using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class MdQnAViewModel
    {
        public int QuesID { set; get; }

        public int AnsID { set; get; }

        public string QuesContentText { set; get; }

        public string QuesContentHTML { set; get; }

        public string AnsContentText { set; get; }

        public string AnsContentHTML { set; get; }

        public string AreaName { set; get; }

        public int AreaID { set; get; }

        public bool IsDelete { set; get; }

        public int Total { set; get; }
    }
}
