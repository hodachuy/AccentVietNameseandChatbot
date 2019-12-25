﻿using System;
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
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Age { set; get; }
        public string Gender { set; get; }
        public string Location { set; get; }
        public string AvatarPicture { set; get; }
        public string PredicateName { set; get; }
        public string PredicateValue { set; get; }
        public string PhoneNumber { set; get; }
		public string EngineerName { set; get; }
		public bool IsHavePredicate { set; get; }
		public bool IsProactiveMessage { set; get; }
        public bool IsHaveCardCondition { set; get; }
        public string CardConditionPattern { set; get; }
        public bool IsConditionWithAreaButton { set; get; }
        public string CardConditionAreaButtonPattern { set; get; }

        public bool IsHaveSetAttributeSystem { set; get; }
        public string AttributeName { set; get; }
        public string CardStepPattern { set; get; }// card chuyển tiếp k cần click khi người dùng nhập giá trị text

        public DateTime StartedOn { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? TimeOut { set; get; }
        public string BranchOTP { set; get; }
    }
}
