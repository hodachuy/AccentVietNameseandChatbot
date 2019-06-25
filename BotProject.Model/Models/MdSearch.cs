using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("MdSearchs")]
    public class MdSearch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Title { set; get; }

        public string Payload { set; get; }

        public int? CardPayloadID { set; get; }

        public string UrlAPI { set; get; }

        public string KeyAPI { set; get; }

        public string MethodeAPI { set; get; }

        public string ParamAPI { set; get; }

        public string MessageStart { set; get; }

        public string MessageError { set; get; }

        public string MessageEnd { set; get; }

        public int? ButtonModuleID { set; get; }

        public int? ModuleFollowCardID { set; get; }

        public int BotID { set; get; }
    }
}
