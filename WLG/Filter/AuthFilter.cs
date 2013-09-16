using System.Web.Mvc;

namespace WLG.Filter
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["ShopId"] == null)
            {
                filterContext.Controller.ViewData["ErrorMsg"] = "没有访问权限,您尚未登录.";
                filterContext.Result = new ViewResult()
                {
                    ViewName = "LogOn",  //Admin
                    ViewData = filterContext.Controller.ViewData
                };
            }
        }
    }
}