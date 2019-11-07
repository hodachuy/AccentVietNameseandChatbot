using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Model.Models
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(50)]
        public string Color { set; get; }

        public string FormName { set; get; }

        [MaxLength(250)]
        public string Logo { set; get; }

        public int? CardID { set; get; }

        public string TextIntroductory { set; get; }

        public bool IsActiveIntroductory { set; get; }

        public bool IsMDSearch { set; get; }

        public bool IsProactiveMessageZalo { set; get; }
		public bool IsProactiveMessageFacebook { set; get; }

		public int Timeout { set; get; }

        public string ProactiveMessageText { set; get; }

        public string FacebookPageToken { set; get; }
        public string FacebookAppSecrect { set; get; }
        public string ZaloPageToken { set; get; }
        public string ZaloAppSecrect { set; get; }
        public string ZaloQRCode { set; get; }

        public int BotID { set; get; }

        public string UserID { set; get; }

        public string StopWord { set; get; }

		public bool IsHaveMaintenance { set; get; }
		public string MessageMaintenance { set; get; }

        //OTP
        public int TimeoutOTP { set; get; }
        public bool IsHaveTimeoutOTP { set; get;}
        public string MessageTimeoutOTP { set; get; }

	}
}
