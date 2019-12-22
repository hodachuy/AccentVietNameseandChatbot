﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
	[Table("AttributeSystems")]
	public class AttributeSystem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }
		public string Name { set; get; }
		public string Type { set; get;}
		public int BotID { set; get; }
	}
}
