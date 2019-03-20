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

        [Required]
        public int BotID { set; get; }

        public virtual IEnumerable<CardContent> CardContents { set; get; }
        public virtual IEnumerable<QuickReply> QuickReplys { set; get; }
    }
    public class QuickReply
    {
        public string ContentType { set; get; }
        public string Icon { set; get; }
        public string Payload { set; get; }
        public string Title { set; get; }
    }

    public class CardContent
    {
       public virtual Message Message { set; get; }
    }
    public class Message
    {
        public virtual TemplateGenericGroup TemplateGenericGroup { set; get; }
        public virtual TemplateText TemplateText { set; get; }
        public virtual Image Image { set; get; }
    }
    public class TemplateGenericGroup
    {
        public string Type { set; get; }
        public virtual IEnumerable<TemplateGenericItem> TemplateGenericItems { set; get; }
    }
    public class TemplateGenericItem
    {
        public string Title { set; get; }
        public string Subtitle { set; get; }
        public string Url { set; get; }
        public string Image { set; get;}
        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }
        public virtual IEnumerable<ButtonPostback> ButtonPostbacks { set; get; }

    }
    public class TemplateText
    {
        public string Text { set; get; }
        public virtual IEnumerable<ButtonLink> ButtonLinks { set; get; }
        public virtual IEnumerable<ButtonPostback> ButtonPostbacks { set; get; }
    }
    public class Image
    {
        public string Url { set; get; }
    }
    public class ButtonLink
    {
        public string Url { set; get; }
        public string SizeHeight { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
    }
    public class ButtonPostback
    {
        public string Payload { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
    }
}