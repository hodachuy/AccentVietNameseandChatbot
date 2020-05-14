using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotProject.Web.Controllers
{
    public class LcChatBoxController : Controller
    {
        // GET: LcOpenChat
        public ActionResult Index(int channelGroupId)
        {
            // Tạo chuỗi ID định danh cho customer nên + channelGroupId,
            // Vì CustomerId lưu storage sẽ k dc thêm vì trùng ID
            ViewBag.CustomerId = Guid.NewGuid().ToString() + "-"+ channelGroupId.ToString();
            ViewBag.ChannelGroupID = channelGroupId;
            return View();
        }
    }
}