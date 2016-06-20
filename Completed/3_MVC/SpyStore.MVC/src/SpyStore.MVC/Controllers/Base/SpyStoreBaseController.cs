using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SpyStore.MVC.Controllers.Base
{
    public class SpyStoreBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Request.Cookies.ContainsKey("CustomerId"))
            {
                ViewBag.CustomerId = Request.Cookies["CustomerId"];
                ViewBag.CustomerName = Request.Cookies["CustomerName"];
            }
            base.OnActionExecuting(context);
        }

    }
}