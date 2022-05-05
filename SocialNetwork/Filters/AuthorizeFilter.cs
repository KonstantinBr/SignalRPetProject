using System;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;

namespace SocialNetwork.Filters
{
    public class InAnonymousAttribute : System.Web.Mvc.ActionFilterAttribute
    {
    }
    public class AuthorizeFilter: System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.GetCustomAttributes(typeof(InAnonymousAttribute), true).Any() || context.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(InAnonymousAttribute), true).Any())
            {
                return;
            }
            int authorizedUserId;
            var hasIdCookie = context.HttpContext.Request.Cookies.AllKeys.Contains("id");
            var RouteDictionary = new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "HelloPage" }
               };
            if (hasIdCookie)
            {
                authorizedUserId = Convert.ToInt32(context.HttpContext.Request.Cookies.Get("id").Value);
                if (authorizedUserId <= 0)
                {
                    context.Result = new RedirectToRouteResult(RouteDictionary);
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(RouteDictionary);
            }
        }
    }
}