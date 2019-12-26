﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.ViewModels
{
    public class StoreProcCardViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public bool IsHaveCondition { set; get; }
        public bool IsConditionWithAreaButton { set; get; }
        public string AttributeSystemName { set; get; }
        public string TemplateJsonFacebook { set; get; }
        public string TemplateJsonZalo { set; get; }
        public int? CardStepID { set; get; }
    }
}
