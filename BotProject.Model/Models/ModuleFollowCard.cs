using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ModuleFollowCards")]
    public class ModuleFollowCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string PartternText { set; get; }

        public int? ModuleInfoPatientID { set; get; }// trường hợp dành cho module Patient get info

        public int Index { set; get; }

        public int? CardID { set; get; }

        public int? BotID { set; get; }
    }
}
