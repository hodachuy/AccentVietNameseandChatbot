using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Common;

namespace BotProject.Web.Infrastructure.Core
{
    public class BaseController : Controller
    {
        private IErrorService _errorService;       

        public BaseController(IErrorService errorService)
        {
            this._errorService = errorService;
        }
        public ApplicationUserViewModel UserInfo
        {
            get
            {
                if (Session != null)
                {
                    if (Session[CommonConstants.SessionUser] != null)
                    {
                        return (ApplicationUserViewModel)Session[CommonConstants.SessionUser];
                    }
                }
                return null;
            }
            set
            {
                if (value == null)
                    Session.Remove(CommonConstants.SessionUser);
                else
                    Session[CommonConstants.SessionUser] = value;
            }
        }
       
        /// <summary>
        /// Check session user null when exceute actionController
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[CommonConstants.SessionUser] == null)
			{
				filterContext.Result = new RedirectResult(Url.Action("Login", "Account", new { returnUrl = CurrentURL }));
				return;
			}
			base.OnActionExecuting(filterContext);
        }
  
        /// <summary>
        /// Log error action controler
        /// </summary>
        /// <param name="exceptionContext"></param>
        protected override void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                string controllerName = (string)exceptionContext.RouteData.Values["controller"];
                string actionName = (string)exceptionContext.RouteData.Values["action"];

                Exception custException = new Exception("There is some error");
                LogError(exceptionContext.Exception, controllerName , actionName);
              

                var model = new HandleErrorInfo(custException, controllerName, actionName);

                exceptionContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = exceptionContext.Controller.TempData
                };

                exceptionContext.ExceptionHandled = true;

            }
        }

        private void LogError(Exception ex, string controllerName, string actionName)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message + "in " + controllerName + "/" + actionName;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }

        protected ActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account", new { ReturnUrl = CurrentURL });
        }
        protected string CurrentURL
        {
            get
            {
                if (ControllerContext.RequestContext.HttpContext.Request.Url.Query.IndexOf("") > -1)
                    return ControllerContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
                return ControllerContext.RequestContext.HttpContext.Request.Url.PathAndQuery;
            }
        }
    }
}