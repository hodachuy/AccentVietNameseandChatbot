using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class StoreProcUserTelephoneByVoucherViewModel
    {
        public int ID { set; get; }
        public string TelephoneNumber { set; get; }
        public bool IsReceived { set; get; }
        public string Title { set; get; }
        public int MdVoucherID { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public string Image { set; get; }
        public string Code { set; get; }
        public int? BotID { set; get; }
        public int Total { set; get; }
    }
}
