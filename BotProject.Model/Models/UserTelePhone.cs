using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("UserTelePhones")]
    public class UserTelePhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string TelephoneNumber { set; get; }
        public string TypeService { set; get; }
        public string Code { set; get; }
        public int NumberReceive { set; get; }
        public bool IsReceive { set; get; }
        public string Type { set; get; }
        public string SerialNumber { set; get; }
        public string NumberOrder { set; get; }
        public int? MdVoucherID { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string BranchOTP { set; get; }
    }
}
