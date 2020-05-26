using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }

        public string FullName { set; get; }

        public DateTime BirthDay { set; get; }

        public string Email { set; get; }

        public string UserName { set; get; }

        public string Avatar { set; get; }

        public string Password { set; get; }

        public string PhoneNumber { set; get; }

        public IEnumerable<ApplicationGroupViewModel> Groups { set; get; }

        public Channel Channels { set; get; }

        public int? BotActiveID { set; get; }
    }
}