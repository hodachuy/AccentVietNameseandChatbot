using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;
using AutoMapper;
using BotProject.Model.Models;
using BotProject.Web.Models;
using System.IO;
using OfficeOpenXml;
using System.Configuration;
using BotProject.Common.ViewModels;
using OfficeOpenXml.Style;
using System.Text;

namespace BotProject.Web.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class BotController : BaseController
    {
        private ICardService _cardService;
		private IQnAService _qnaService;
        private ISettingService _settingService;
        private IGroupCardService _groupCardService;
        private IModuleService _moduleService;
        private IModuleSearchEngineService _mdSearchEngineService;
        private IBotService _botService;
        private BotServiceMedical _botServiceMed;
        public BotController(IErrorService errorService,
            ICardService cardService,
            IQnAService qnaService,
            ISettingService settingService,
            IGroupCardService groupCardService,
            IModuleService moduleService,
            IBotService botService,
            IModuleSearchEngineService mdSearchEngineService
           ) : base(errorService)
        {
            _cardService = cardService;
			_qnaService = qnaService;
            _settingService = settingService;
            _groupCardService = groupCardService;
            _moduleService = moduleService;
            _mdSearchEngineService = mdSearchEngineService;
            _botService = botService;

        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int formQnAId, int botId, string botName)
		{
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index","Dashboard");
            }
            if(botId == 3019)
            {
                _botServiceMed = BotServiceMedical.BotInstance;
            }
            //_accentService = new AccentService();// AccentService.AccentInstance;

            var formQnA = _qnaService.GetFormQnAnswerById(formQnAId);
            var formQnAVm = Mapper.Map<FormQuestionAnswer, FormQuestionAnswerViewModel>(formQnA);

            string strCards = "";
            StringBuilder sb = new StringBuilder();
            var lstGroupCard = _groupCardService.GetListGroupCardByBotID(botId).ToList();
            if (lstGroupCard.Count() != 0)
            {
                foreach (var item in lstGroupCard)
                {
                    sb.Append("<optgroup label=\"" + item.Name.ToUpper() + "\">");
                    var lstCard = _cardService.GetListCardByGroupCardID(item.ID).ToList();
                    if(lstCard.Count() != 0)
                    {
                        foreach(var iCard in lstCard)
                        {
                            sb.Append("<option value=\"" + iCard.ID + "\"> " + iCard.Name + "</option>");
                        }
                    }
                    //item.Cards = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(lstCard);
                    sb.Append("</optgroup>");
                }
                strCards = sb.ToString();
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            formQnAVm.StrTempOtpCards = serializer.Serialize(strCards).TrimStart('"').TrimEnd('"');

            ViewBag.BotQnAnswerID = formQnAId;
            ViewBag.BotName = botName;

            return View(formQnAVm);
		}

        public ActionResult Module(int id, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotID = id;
            ViewBag.BotName = botName;
            return View();
        }

        public ActionResult CardCategory(int id, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotID = id;
            ViewBag.BotName = botName;
            var lstModule = _moduleService.GetAllModuleByBotID(id);
            var lstModuleVm = Mapper.Map<IEnumerable<Module>, IEnumerable<ModuleViewModel>>(lstModule);
            return View(lstModuleVm);
		}

		public ActionResult AIML(int id)
		{
            ViewBag.BotID = id;
            return View();
		}

        public ActionResult Setting(int id, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var settingDb = _settingService.GetSettingByBotID(id);

            var settingVm = Mapper.Map<Setting, BotSettingViewModel>(settingDb);
            if (settingVm.CardID != null)
            {
                settingVm.CardName = _cardService.GetCardByPattern("postback_card_" + settingVm.CardID).Name;
            }

            var lstBot = _botService.GetListBotByUserID(UserInfo.Id);
            var lstBotVm = Mapper.Map<IEnumerable<Bot>,IEnumerable<BotViewModel>>(lstBot);
            var lstSystemConfig = _settingService.GetListSystemConfigByBotId(id);
            ViewBag.BotName = name;
            ViewBag.UserID = UserInfo.Id;
            ViewBag.Bots = lstBotVm;
            ViewBag.SystemConfigs = lstSystemConfig;
            return View(settingVm);
        }

        public ActionResult FormChatSetting(string botName)
        {
            ViewBag.BotName = botName;
            return View();
        }

        public ActionResult BotSearchEngine(int botId, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotName = botName;
            ViewBag.BotID = botId;
            var lstMdArea = _mdSearchEngineService.GetListMdArea(botId).ToList();
            ViewBag.MdArea = lstMdArea;
            return View();
        }

        public ActionResult BotHistory(int botId, string botName)
        {
            ViewBag.BotName = botName;
            ViewBag.BotID = botId;
            return View();
        }

        public ActionResult BotMedicalSymptoms(int botId, string botName)
        {
            ViewBag.BotName = botName;
            ViewBag.BotID = botId;
            return View();
        }


        [ValidateInput(false)]
        public JsonResult ExcelVoucherView(UserTelephoneExport[] Data)
        {
            string path = ConfigurationManager.AppSettings["ExcelTemplatePath"];
            string pathtempt = ConfigurationManager.AppSettings["ExcelTemplatePath"];
            string exname = ConfigurationManager.AppSettings["StatisticExcelVoucher"];
            string ext = ConfigurationManager.AppSettings["ExcelExtension"];
            string fileNameTemplate = path + exname + ext;
            string fileName = exname + DateTime.Now.Ticks.ToString() + ext;
            string pathfileName = pathtempt + fileName;

            FileInfo filename = new FileInfo(@fileNameTemplate);

            using (ExcelPackage pck = new ExcelPackage(filename))
            {
                ExcelWorkbook workBook = pck.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        var color = System.Drawing.Color.FromArgb(218, 238, 243);
                        ExcelWorksheet cSheet = workBook.Worksheets[1];

                        int r = 4;
                        for (int i = 0; i < Data.Length; i++)
                        {
                            UserTelephoneExport item = Data[i];
                            // Thêm  ngày/tháng/năm, Người tạo.
                            cSheet.Cells[2, 2].Value = "";
                            cSheet.Cells[2, 4].Value = item.StartDate;
                            cSheet.Cells[2, 6].Value = item.EndDate;
                            cSheet.Cells[2, 8].Value = DateTime.Now.ToString("dd/MM/yyyy");

                            cSheet.Cells[r, 1].Value = i + 1;
                            cSheet.Cells[r, 2].Value = item.NumberOrder;
                            if (item.IsReceived)
                            {
                                cSheet.Cells[r, 3].Value = "Đã nhận";
                            }else
                            {
                                cSheet.Cells[r, 3].Value = "Chưa nhập OTP";
                            }

                            cSheet.Cells[r, 4].Value = item.TelephoneNumber;
                            cSheet.Cells[r, 5].Value = item.CodeOTP;          
                            if(!String.IsNullOrEmpty(item.SerialNumber) && item.SerialNumber.Contains("postback"))
                            {
                                item.SerialNumber = "";
                            }    
                            cSheet.Cells[r, 6].Value = item.SerialNumber;
                            cSheet.Cells[r, 7].Value = item.CreatedDate;
                            cSheet.Cells[r, 8].Value = item.Type;
                            cSheet.Cells[r, 9].Value = item.BranchOTP;
                            if ((i + 1) % 2 == 0)
                            {
                                cSheet.Cells[r, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 1].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 2].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 3].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 4].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 5].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 6].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 7].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 8].Style.Fill.BackgroundColor.SetColor(color);
                                cSheet.Cells[r, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cSheet.Cells[r, 9].Style.Fill.BackgroundColor.SetColor(color);

                            }
                            cSheet.Cells[r, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            r++;
                        }
                    }
                }
                pck.SaveAs(new FileInfo(@pathfileName));
            }

            CustomFile file = new CustomFile();
            file.FileName = fileName;
            file.FilePath = pathtempt;

            return Json(new { Table = file });
        }

        public FileResult Download(CustomFile file)
        {
            //string file = @"c:\someFolder\foo.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file.FilePath + file.FileName, contentType, file.FileName);
        }
    }
    public class CustomFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}