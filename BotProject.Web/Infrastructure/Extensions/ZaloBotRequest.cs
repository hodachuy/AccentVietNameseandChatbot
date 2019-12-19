using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Infrastructure.Extensions
{
    public class ZaloBotRequest
    {
        public string event_name { set; get; }
        public string app_id { set; get; }
        public string oa_id { set; get; }
        public UserRequest sender { set; get; }
        public UserRequest recipient { set; get; }
        public MessageReceivedRequest message { set; get; }
        public string timestamp { set; get; }
        public string user_id_by_app { set; get; }
        public string source { set; get; }
        public UserRequest follower { set; get; }
    }
    public class UserRequest
    {
        public string id { set; get; }
    }
    public class MessageReceivedRequest
    {
        public string text { set; get; }
        public string msg_id { set; get; }

        public List<MessageAttachmentZalo> attachments { get; set; }
    }
    public class MessageAttachmentZalo
    {
        public string type { get; set; }
        public MessageAttachmentPayLoadZalo payload { get; set; }
    }
    public class MessageAttachmentPayLoadZalo
    {
        public string url { get; set; }
        public string size { get; set; }
        public string name { get; set; }
        public string checksum { get; set; }
        public string type { get; set; }
    }
}