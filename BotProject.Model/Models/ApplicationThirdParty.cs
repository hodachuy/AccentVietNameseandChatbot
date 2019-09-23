using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ApplicationThirdParties")]
    public class ApplicationThirdParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string PageID { set; get; }
        public string Type { set; get; }
        public string AccessToken { set; get; }
        public string SecrectKey { set; get; }
        public int BotID { set; get; }
        public bool IsDelete { set; get; }
        public bool IsActive { set; get; }
    }
}
