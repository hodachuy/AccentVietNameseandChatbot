using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Models
{
    public class ApplicationFacebookUserViewModel
    {
		public int ID { set; get; }
		public string UserId { set; get; }
		public string UserName { set; get; }
		public string PredicateName { set; get; }
		public string PredicateValue { set; get; }
		public string PhoneNumber { set; get; }
		public string EngineerName { set; get; }
		public bool IsHavePredicate { set; get; }
		public bool IsProactiveMessage { set; get; }
		public DateTime StartedOn { set; get; }
		public DateTime? TimeOut { set; get; }
	}
}