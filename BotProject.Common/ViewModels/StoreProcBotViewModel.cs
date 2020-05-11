using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class StoreProcBotViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }

        public string Alias { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }
        public bool IsActiveLiveChat { set; get; }
        public bool Status { set; get; }
        public string UserID { set; get; }
        public int TotalCard { set; get; }
        public int TotalQuestionScript { set; get; }
        public int TotalIntent { set; get; }
        public string FacebookToken { set; get; }
        public string ZaloToken { set; get; }
    }
}
