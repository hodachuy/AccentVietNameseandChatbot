using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Common
{
    public class WarrantyResultModel
    {
        public string ServiceTag { get; set; }
        public DateTime ShipDate { get; set; }
        public string Country { get; set; }

        public string TextWarranty { get; set; }

        public List<WarrantyDetailModel> WarrantyDetails { get; set; }
    }

    public class WarrantyDetailModel
    {
        public string Service { get; set; }
        public string ServiceLevelCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EntitlementType { get; set; }
        public int Priority { get; set; }
    }
}