using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Common
{
    public struct AssetWarrantyResponse
    {
        public class AssetWarrantyResponseModel
        {
            public AssetHeaderModel AssetHeader { get; set; }
            public ProductHeaderModel ProductHeader { get; set; }
            public List<AssetEntitlementModel> AssetEntitlement { get; set; }
        }
        public class AssetEntitlementModel
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string ServiceLevelDescription { get; set; }
            public string ServiceLevelCode { get; set; }
            public string ServiceLevelGroup { get; set; }
            public string EntitlementType { get; set; }
            public string ServiceProvider { get; set; }
            public string ItemNumber { get; set; }
        }

        public class AssetHeaderModel
        {
            public string BUILD { get; set; }
            public string ServiceTag { get; set; }
            public DateTime ShipDate { get; set; }
            public string CountryLookupCode { get; set; }
            public string LocalChannel { get; set; }
            public string CustomerNumber { get; set; }
            public string ItemClassCode { get; set; }
            public bool IsDuplicate { get; set; }
            public string MachineDescription { get; set; }
            public string OrderNumber { get; set; }
            public string ParentServiceTag { get; set; }
            public string CompanyNumber { get; set; }
        }

        public class ProductHeaderModel
        {
            public string SystemDescription { get; set; }
            public string ProductId { get; set; }
            public string ProductFamily { get; set; }
            public string LOB { get; set; }
            public string LOBFriendlyName { get; set; }
        }
    }
}