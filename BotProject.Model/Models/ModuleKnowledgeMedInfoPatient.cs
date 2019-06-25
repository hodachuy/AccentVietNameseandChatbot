using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotProject.Model.Models
{
    public class ModuleKnowledgeMedInfoPatient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Title { set; get; }

        public bool IsCheck { set; get; }

        public string OptionText { set; get; }

        public string Payload { set; get; }

        public int? CardPayloadID { set; get; }

        public string MessageEnd { set; get; }

        public string Key { set; get; }

        public int? ButtonModuleID { set; get; }

        public int? ModuleFollowCardID { set; get; }

        public int ModuleID { set; get; }

        public int BotID { set; get; }

    }
}
