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
    }
    public class TemplateGenericGroupViewModel
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public virtual IEnumerable<TemplateGenericItemViewModel> TemplateGenericItemViewModels { set; get; }
    }
    public class TemplateGenericItemViewModel
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public string Subtitle { set; get; }
        public string Url { set; get; }
        public string Image { set; get;}
        public int? AttachmentID { set; get; }
        public virtual IEnumerable<ButtonLinkViewModel> ButtonLinkViewModels { set; get; }
        public virtual IEnumerable<ButtonPostbackViewModel> ButtonPostbackViewModels { set; get; }

    }
    public class TemplateTextViewModel
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public string Text { set; get; }
        public virtual IEnumerable<ButtonLinkViewModel> ButtonLinkViewModels { set; get; }
        public virtual IEnumerable<ButtonPostbackViewModel> ButtonPostbackViewModels { set; get; }
    }
    public class ButtonLinkViewModel
    {
        public int ID { set; get; }
        public string Url { set; get; }
        public string SizeHeight { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
    }
    public class ButtonPostbackViewModel
    {
        public int ID { set; get; }
        public string Payload { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
        public int? CardPayloadID { set; get; }
    }
    public class FileAttach
    {
        public string attachment_url { set; get; }
        public int attachment_id { set; get; }
        public string type { set; get; }
    }
}