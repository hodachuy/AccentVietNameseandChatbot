﻿using System;
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
            ViewBag.CustomerId = Guid.NewGuid().ToString() + "_"+ channelGroupId.ToString();
            ViewBag.ChannelGroupID = channelGroupId;

            string customer_Id = string.Empty;
            HttpCookie reqCookies = Request.Cookies["customerInfo"];
            if(reqCookies == null)
            {
                HttpCookie customerInfo = new HttpCookie("customerInfo");
                customerInfo["customerId"] = Guid.NewGuid().ToString() + "_" + channelGroupId.ToString();
                customerInfo.Expires = DateTime.Now.AddMinutes(2);
                Response.Cookies.Add(customerInfo);
            }
            else
            {
                customer_Id = reqCookies["customerId"].ToString();
            }

            return View();
        }
    }
}