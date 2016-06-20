using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SpyStore.MVC.DataAccess;

namespace SpyStore.MVC.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.CartReturnUrl = Request.Path;
            return View("LoginView");
        }
    }
}
