using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BotProject.Common.AssetWarrantyResponse;

namespace BotProject.Common
{
    public class DellServices
    {
        private static string UrlDell = ConfigHelper.ReadString("UrlDell");

        private static Dictionary<string, int> ServicePrioritize = new Dictionary<string, int>() {
            {"2HR", 1},
            {"4HR", 2},
            {"6HR", 3},
            {"NBD", 4},
            {"CIS", 5},
            {"POW", 6}
        };

        public static WarrantyResultModel GetAssetHeader(string ServiceTag)
        {
            if (!String.IsNullOrEmpty(ServiceTag))
            {
                String URL = String.Format("getassetwarranty/{0}?apikey=1b31b0bf-980d-49eb-b77b-2c135ceb6974", ServiceTag);
                string jsonResult = GetAsync(URL).Result;
                if (!String.IsNullOrEmpty(jsonResult))
                {
                    AssetWarrantyResponseModel assetWarrantyResponseModel = ConvertJsonToAssetWarrantyResponse(jsonResult);
                    if (assetWarrantyResponseModel.AssetHeader != null)
                    {
                        WarrantyResultModel warrantyResultModel = new WarrantyResultModel();
                        WarrantyDetailModel warrantyDetailModel = new WarrantyDetailModel();
                        List<WarrantyDetailModel> warrantyDetailList = new List<WarrantyDetailModel>();
                        warrantyResultModel.ServiceTag = assetWarrantyResponseModel.AssetHeader.ServiceTag;
                        warrantyResultModel.Country = assetWarrantyResponseModel.AssetHeader.CountryLookupCode;
                        warrantyResultModel.ShipDate = assetWarrantyResponseModel.AssetHeader.ShipDate;
                        if(assetWarrantyResponseModel.AssetEntitlement != null)
                        {
                            foreach (var item in assetWarrantyResponseModel.AssetEntitlement)
                            {
                                warrantyDetailModel = new WarrantyDetailModel();
                                warrantyDetailModel.Service = (item.ServiceLevelCode + " (" + item.ServiceLevelDescription + ")");
                                warrantyDetailModel.ServiceLevelCode = item.ServiceLevelCode;
                                warrantyDetailModel.EntitlementType = item.EntitlementType;
                                warrantyDetailModel.StartDate = item.StartDate;
                                warrantyDetailModel.ExpirationDate = item.EndDate;
                                warrantyDetailList.Add(warrantyDetailModel);                           
                            }
                            //Xử lý danh sách warrantyDetail
                            var WarrantyDetailResult = GroupWarrantyDetail(warrantyDetailList);
                            warrantyResultModel.WarrantyDetails = WarrantyDetailResult.OrderBy(x=>x.ExpirationDate).OrderBy(x => x.Priority).ToList();
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Thông tin bảo hành của máy có Service tag "+ warrantyResultModel.ServiceTag +":" +"<br/><br/>");
                            sb.AppendLine("- Model: " + assetWarrantyResponseModel.AssetHeader.MachineDescription + "<br/>");
                            sb.AppendLine("- Ngày ship: " + assetWarrantyResponseModel.AssetHeader.ShipDate.ToString("dd/MM/yyyy") + "<br/>");
                            sb.AppendLine("- Quốc gia: " + assetWarrantyResponseModel.AssetHeader.CountryLookupCode + "<br/>");
                            sb.AppendLine("- Ngày hết hạn BH ("+ warrantyResultModel.WarrantyDetails[0].ServiceLevelCode + "): " + warrantyResultModel.WarrantyDetails[0].ExpirationDate.ToString("dd/MM/yyyy") + "<br/>");

                            //sb.AppendLine("Service Tag: " + warrantyResultModel.ServiceTag + "<br/>");
                            //sb.AppendLine("Thời hạn bảo hành: " + warrantyResultModel.WarrantyDetails[0].ExpirationDate.ToString("dd MMM yyyy") + "<br/>");
                            //sb.AppendLine("Thông tin chi tiết: <br/>");
                            //foreach (var obj in warrantyResultModel.WarrantyDetails)
                            //{
                            //    sb.AppendLine(obj.Service + "<br/>");
                            //    sb.AppendLine("Sta: "+obj.StartDate.ToString("dd MMM yyyy") + " ,Exp: "+obj.ExpirationDate.ToString("dd MMM yyyy")+"<br/>");
                            //}

                            warrantyResultModel.TextWarranty = sb.ToString();
                            return warrantyResultModel;
                        }
                    }
                }
            }
            return null;
        }
        private static async Task<String> GetAsync(String url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlDell);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                //Logger.SlackApplication.PutExceptionMessage(exception, Characters.SlackWebhookURL.DGP_API_SentSMS);
                return null;
            }
        }
        private static AssetWarrantyResponseModel ConvertJsonToAssetWarrantyResponse(string Json)
        {
            AssetWarrantyResponseModel ResultModel = new AssetWarrantyResponseModel();
            AssetHeaderModel assetHeader = new AssetHeaderModel();
            ProductHeaderModel productHeader = new ProductHeaderModel();
            AssetEntitlementModel assetEntitlement = new AssetEntitlementModel();
            List<AssetEntitlementModel> AssetEntitlementModelList = new List<AssetEntitlementModel>();

            JObject jObject = JObject.Parse(Json);
            JToken jAssetEntitlement = jObject.SelectToken("AssetWarrantyResponse[0].AssetEntitlementData");
            JToken jAssetHeader = jObject.SelectToken("AssetWarrantyResponse[0].AssetHeaderData");
            JToken jProductHeader = jObject.SelectToken("AssetWarrantyResponse[0].ProductHeaderData");

            if (jAssetHeader != null && jAssetHeader.HasValues)
            {
                assetHeader.BUILD = (string)jAssetHeader["BUID"];
                assetHeader.ServiceTag = (string)jAssetHeader["ServiceTag"];
                assetHeader.ShipDate = (DateTime)jAssetHeader["ShipDate"];
                assetHeader.CountryLookupCode = (string)jAssetHeader["CountryLookupCode"];
                assetHeader.LocalChannel = (string)jAssetHeader["LocalChannel"];
                assetHeader.CustomerNumber = (string)jAssetHeader["CustomerNumber"];
                assetHeader.ItemClassCode = (string)jAssetHeader["ItemClassCode"];
                assetHeader.IsDuplicate = (bool)jAssetHeader["IsDuplicate"];
                assetHeader.MachineDescription = (string)jAssetHeader["MachineDescription"];
                assetHeader.OrderNumber = (string)jAssetHeader["OrderNumber"];
                assetHeader.ParentServiceTag = (string)jAssetHeader["ParentServiceTag"];
                assetHeader.CompanyNumber = (string)jAssetHeader["CompanyNumber"];
                ResultModel.AssetHeader = assetHeader;
            }

            if (jAssetHeader != null && jProductHeader.HasValues)
            {
                productHeader.SystemDescription = (string)jProductHeader["SystemDescription"];
                productHeader.ProductId = (string)jProductHeader["ProductId"];
                productHeader.ProductFamily = (string)jProductHeader["ProductFamily"];
                productHeader.LOB = (string)jProductHeader["LOB"];
                productHeader.LOBFriendlyName = (string)jProductHeader["LOBFriendlyName"];
                ResultModel.ProductHeader = productHeader;
            }

            if (jAssetHeader != null && jAssetEntitlement.HasValues)
            {
                for (int i = 0; i < jAssetEntitlement.Count(); i++)
                {
                    string _quantityJtoken = "AssetWarrantyResponse[0].AssetEntitlementData[" + i + "]";
                    JToken jAssetEntitlementChild = jObject.SelectToken(_quantityJtoken);
                    assetEntitlement = new AssetEntitlementModel();
                    assetEntitlement.StartDate = (DateTime)jAssetEntitlementChild["StartDate"];
                    assetEntitlement.EndDate = (DateTime)jAssetEntitlementChild["EndDate"];
                    assetEntitlement.ServiceLevelDescription = (string)jAssetEntitlementChild["ServiceLevelDescription"];
                    assetEntitlement.ServiceLevelCode = (string)jAssetEntitlementChild["ServiceLevelCode"];
                    assetEntitlement.ServiceLevelGroup = (string)jAssetEntitlementChild["ServiceLevelGroup"];
                    assetEntitlement.EntitlementType = (string)jAssetEntitlementChild["EntitlementType"];
                    assetEntitlement.ServiceProvider = (string)jAssetEntitlementChild["ServiceProvider"];
                    assetEntitlement.ItemNumber = (string)jAssetEntitlementChild["ItemNumber"];
                    AssetEntitlementModelList.Add(assetEntitlement);
                }
                ResultModel.AssetEntitlement = AssetEntitlementModelList;
            }
            return ResultModel;
        }
        //Gộp danh sách WarrantyDetail
        private static List<WarrantyDetailModel> GroupWarrantyDetail(List<WarrantyDetailModel> inputList)
        {
            List<WarrantyDetailModel> warrantyDetailsResult = new List<WarrantyDetailModel>();
            List<WarrantyDetailModel> warrantyDetailTemp = new List<WarrantyDetailModel>();
            WarrantyDetailModel inputModel = new WarrantyDetailModel();

            var ServicesList = GetServicesType(inputList);
            foreach (var item in ServicesList)
            {
                int priorityDefault = 10;
                foreach(var itemPrioritize in ServicePrioritize)
                {
                    if (item.Contains(itemPrioritize.Key))
                    {
                        priorityDefault = itemPrioritize.Value;
                    }
                }
                warrantyDetailTemp = inputList.Where(x => x.Service == item).ToList();
                DateTime _beginDate = DateTime.Now;
                DateTime _expirationDate = DateTime.Now;
                GetBeginAndExpirationDate(warrantyDetailTemp, out _beginDate, out _expirationDate);
                inputModel = new WarrantyDetailModel
                {
                    Service = item,
                    ServiceLevelCode = warrantyDetailTemp[0].ServiceLevelCode,
                    EntitlementType = "",
                    StartDate = _beginDate,
                    ExpirationDate = _expirationDate,
                    Priority = priorityDefault
                };
                if(item.Contains("(Dell Digitial Delivery)") == false)
                {
                    warrantyDetailsResult.Add(inputModel);
                }
            }
            return warrantyDetailsResult;
        }
        //Lấy danh sách Services
        private static List<String> GetServicesType(List<WarrantyDetailModel> inputList)
        {
            var EntitlementTypeList = inputList.Select(x => x.Service).ToList().GroupBy(x => x).Select(x => x.Key).ToList();
            return EntitlementTypeList;
        }
        //Lấy Begin và Expiration
        private static void GetBeginAndExpirationDate(List<WarrantyDetailModel> inputList, out DateTime BeginDate, out DateTime ExpirationDate)
        {
            DateTime beginDate = inputList.Where(x => x.StartDate <= DateTime.Now).OrderByDescending(x => x.ExpirationDate).FirstOrDefault().StartDate;
            DateTime expirationDate = inputList.Max(x => x.ExpirationDate);
            BeginDate = beginDate;
            ExpirationDate = expirationDate;
        }
    }
}