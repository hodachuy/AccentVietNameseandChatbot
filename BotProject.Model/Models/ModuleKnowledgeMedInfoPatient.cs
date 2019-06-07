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

        public string Text { set; get; }

        public bool IsCheck { set; get; }

        public int PatternText { set; get; }

        public int? CardID { set; get; }

        public int? ButtonModuleID { set; get; }

        public int ModuleID { set; get; }

        public int BotID { set; get; }

    }
}
