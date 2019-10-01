using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("MdAdminContacts")]
	public class MdAdminContact
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }
		public string Title { set; get; }
		public string TitlePayload { set; get; }
		public string Payload { set; get; }
		public int? CardPayloadID { set; get; }
		public string DictionaryKey { set; get; }
		public string DictionaryValue { set; get; }
		public string MessageStart1 { set; get; }
		public string MessageStart2 { set; get; }
		public string MessageStart3 { set; get; }
		public int ModuleID { set; get; }
		public int BotID { set; get; }
	}
}
