using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.DigiproService.Digipro.Model
{
    public class DigiproServiceModel
    {
        public int rofnumber { set; get; }
        public string servicetag { set; get; }
        public string customername { set; get; }
        public string phonenumber { set; get; }
        public string address { set; get; }
        public string email { set; get; }
        public DateTime? datereceive { set; get; }
        public DateTime? datetest { set; get; }
        public DateTime? dateeta { set; get; }
        public DateTime? datecomplete { set; get; }
        public DateTime? dateclose { set; get; }
        public int id_error { set; get; }
        public int id_sla { set; get; }
    }
}
