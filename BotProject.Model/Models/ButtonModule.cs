using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ButtonModules")]
    public class ButtonModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        // Render  thẻ button module trên giao diện gồm data-postback, nếu type handle, moduleID và moduleknowledgeid nếu ccó
        public int ID { set; get; }

        public string Type { set; get; }

        public string Payload { set; get; }//postback_module

        public string Title { set; get; }

        public int Index { set; get; }

        public int? TempGnrItemID { set; get; }

        public int? TempTxtID { set; get; }

        public int? ModuleID { set; get; }

        public int? ModuleKnowledgeID { set; get; }

        public int? MdSearchID { set; get; }

        public string ModuleType { set; get; }

        public int? CardID { set; get; }
    }
}
