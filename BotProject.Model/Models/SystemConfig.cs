using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotProject.Model.Models
{
    [Table("SystemConfigs")]
    public class SystemConfig
    {
        [Key]
        public int ID { set; get; }

        [Required]
        [Column(TypeName = "varchar")]
        public string Code { set; get; }

        public string ValueString { set; get; }

        public int? ValueInt { set; get; }

        public int BotID { set; get; }
    }
}