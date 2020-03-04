using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class StoreProcQuesGroupViewModel
    {
        public int ID { set; get; }

        public int Index { set; get; }

        public bool? IsKeyword { set; get; }

        public DateTime? CreatedDate { set; get; }

        public int BotID { set; get; }

        public int FormQuestionAnswerID { set; get; }
        public int Total { set; get; }
    }
}
