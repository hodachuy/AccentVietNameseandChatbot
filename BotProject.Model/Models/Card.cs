using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Cards")]
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public string Alias { set; get; }

        public string PatternText { set; get; }// postback_card_id

        public string TemplateAIML { set; get; }

        public string TemplateHTML { set; get; }

        public string TemplateJSON { set; get; }

        public string TemplateJsonFacebook { set; get; }

        public string TemplateJsonZalo { set; get; }

        public int BotID { set; get; }

        public bool IsDelete { set; get; }

        public bool Status { set; get; }

        public bool IsHaveCondition { set; get; }

        public bool IsConditionWithAreaButton { set; get; }

        public bool IsConditionWithInputText { set; get; }

        public string AttributeSystemName { set; get; }// biến lưu

        public int? CardStepID { set; get; } // card đi tiếp không phải click

        [Required]
        public int GroupCardID { set; get; }

        [ForeignKey("GroupCardID")]
        public virtual GroupCard GroupCard { set; get; }

        public virtual IEnumerable<Answer> Answers { set; get; }

        public virtual IEnumerable<TemplateGenericGroup> TemplateGenericGroups { set; get; }

        public virtual IEnumerable<TemplateText> TemplateTexts { set; get; }

        public virtual IEnumerable<Image> Images { set; get; }

        public virtual IEnumerable<FileDocument> FileDocuments { set; get; }

        public virtual IEnumerable<ModuleFollowCard> ModuleFollowCards { set; get; }

        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }

        public virtual IEnumerable <ButtonPostback> ButtonPostbacks { set; get; }

        public virtual IEnumerable<QuickReply> QuickReplys { set; get; }
    }
}
