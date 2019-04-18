using System;
using System.Web.Mvc;

public class AllowCrossSiteAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Allow header domain call to action controller
    /// *,http://qa.surelrn.vn
    /// </summary>
    /// <param name="filterContext"></param>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "*");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

        base.OnActionExecuting(filterContext);
    }
}