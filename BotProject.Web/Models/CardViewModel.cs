using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class CardViewModel
    {
        public int? ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }
        public string TemplateJSON { set; get; }
        public string TemplateJsonFacebook { set; get; }
        public string TemplateJsonZalo { set; get; }

        public bool IsDelete { set; get; }
        public bool Status { set; get; }
        public bool IsHaveCondition { set; get; }
        public bool IsConditionWithAreaButton { set; get; }

        public string UserID { set; get; }
        [Required]
        public int GroupCardID { set; get; }
        [Required]
        public int BotID { set; get; }

        public virtual IEnumerable<CardContent> CardContents { set; get; }
        public virtual IEnumerable<QuickReplyViewModel> QuickReplyViewModels { set; get; }
        public virtual IEnumerable<FileAttach> FileAttachs { set; get; }
    }
    public class QuickReplyViewModel
    {
        public int ID { set; get; }
        public string ContentType { set; get; }
        public string Icon { set; get; }
        public string Payload { set; get; }
        public int Index { set; get; }
        public int? CardPayloadID { set; get; }
        public string Title { set; get; }
    }

    public class CardContent
    {
       public virtual Message Message { set; get; }
    }
    public class Message
    {
        public virtual TemplateGenericGroupViewModel TemplateGenericGroupViewModel { set; get; }
        public virtual TemplateTextViewModel TemplateTextViewModel { set; get; }
        public virtual ImageViewModel ImageViewModel { set; get; }
        public virtual FileDocumentViewModel FileDocumentViewModel { set; get; }
        public virtual ModuleFollowCardViewModel ModuleFollowCardViewModel { set; get; }
    }
    public class TemplateGenericGroupViewModel
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public int Index { set; get; }
        public virtual IEnumerable<TemplateGenericItemViewModel> TemplateGenericItemViewModels { set; get; }
    }
    public class TemplateGenericItemViewModel
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public string Subtitle { set; get; }
        public string Url { set; get; }
        public string Image { set; get;}
        public int Index { set; get; }

        public int? AttachmentID { set; get; }
        public virtual IEnumerable<ButtonLinkViewModel> ButtonLinkViewModels { set; get; }
        public virtual IEnumerable<ButtonPostbackViewModel> ButtonPostbackViewModels { set; get; }
        public virtual IEnumerable<ButtonModuleViewModel> ButtonModuleViewModels { set; get; }
    }

    public class TemplateTextViewModel
    {
        public int ID { set; get; }
        public string Type { set; get; }

        public int Index { set; get; }
        public string Text { set; get; }
        public virtual IEnumerable<ButtonLinkViewModel> ButtonLinkViewModels { set; get; }
        public virtual IEnumerable<ButtonPostbackViewModel> ButtonPostbackViewModels { set; get; }
        public virtual IEnumerable<ButtonModuleViewModel> ButtonModuleViewModels { set; get; }
    }
    public class ButtonLinkViewModel
    {
        public int ID { set; get; }
        public string Url { set; get; }
        public string SizeHeight { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
        public int Index { set; get; }
    }
    public class ButtonPostbackViewModel
    {
        public int ID { set; get; }
        public string Payload { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
        public int Index { set; get; }
        public int? CardPayloadID { set; get; }
        public string DictionaryKey { set; get; }
        public string DictionaryValue { set; get; }
    }

    public class ButtonModuleViewModel
    {
        public int ID { set; get; }

        public string Type { set; get; }

        public string Payload { set; get; }

        public string Title { set; get; }

        public int Index { set; get; }

        public int? TempGnrItemID { set; get; }

        public int? TempTxtID { set; get; }

        public int? ModuleID { set; get; }

        public int? ModuleKnowledgeID { set; get; }

        public int? MdSearchID { set; get; }

        public int? MdVoucherID { set; get; }

        public string ModuleType { set; get; }

        public int? CardID { set; get; }
    }
    public class FileAttach
    {
        public string attachment_url { set; get; }
        public int attachment_id { set; get; }
        public string type { set; get; }
    }
}