using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("Notifies")]
	public class Notify
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }
		[MaxLength(256)]
		public string Message { set; get; }
		public int? SenderID { set; get; }
		public int? RecipientID { set; get; }
		public bool? IsRead { set; get; }
		[MaxLength(50)]
		public string TableName { set; get; }
		public int? TableID { set; get; }
		public DateTime? CreatedDate { set; get; }

		[MaxLength(256)]
		public string URL { set; get; }

	}
}
