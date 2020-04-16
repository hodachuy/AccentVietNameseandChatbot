using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(15)]
        public string IPAddress { get; set; }
        [MaxLength(150)]
        public string City { get; set; }
        [MaxLength(150)]
        public string Region { get; set; }
        [MaxLength(150)]
        public string Country { get; set; }
        [MaxLength(150)]
        public string Latitude { get; set; }
        [MaxLength(150)]
        public string Longtitude { get; set; }
        [MaxLength(150)]
        public string Timezone { get; set; }
        public string FullUserAgent { get; set; }
        [MaxLength(50)]
        public string OS { get; set; }
        [MaxLength(150)]
        public string Browser { get; set; }
        public bool IsMobile { get; set; }

        [Required]
        public string CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }
    }
}
