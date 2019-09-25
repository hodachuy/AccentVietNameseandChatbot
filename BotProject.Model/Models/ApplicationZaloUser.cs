using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("ApplicationZaloUsers")]
    public class ApplicationZaloUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }
        public string PredicateName { set; get; }
        public string PredicateValue { set; get; }
        public string PhoneNumber { set; get; }
        public bool IsHavePredicate { set; get; }
        public bool IsProactiveMessage { set; get; }
        public DateTime StartedOn { set; get; }
    }
}
