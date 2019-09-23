using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("AppFacebookUsers")]
	public class AppFacebookUser
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { set; get; }
		public string UserId { set; get; }
		public string PredicateName { set; get; }
		public string PredicateValue { set; get; }
		public bool PredicateIsCheck { set; get; }
		public DateTime StartedOn { set; get; }
		public bool MessageIsProactived { set; get; }
	}
}
