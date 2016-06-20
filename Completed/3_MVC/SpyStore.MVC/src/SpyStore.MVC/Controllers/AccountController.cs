using System;
using System.Threading.Tasks;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;
using SpyStore.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SpyStore.MVC.Controllers
{
    public class AccountController : SpyStoreBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var customers = await WebAPICalls.GetCustomers();
            return View(new LoginViewModel() {Customers = customers});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null, string method = "get")
        {
            ViewData["ReturnUrl"] = returnUrl;
            Response.Cookies.Delete("CustomerId", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            Response.Cookies.Delete("CustomerName", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            if (!ModelState.IsValid) return View(model);
            var customer = await WebAPICalls.GetCustomer(model.CustomerId);
            Response.Cookies.Append("CustomerId",customer.Id.ToString());
            Response.Cookies.Append("CustomerName",customer.FullName);

            //var principal = new ClaimsPrincipal(
            //    new ClaimsIdentity(GetClaims(customer)));
            //await HttpContext.Authentication.SignInAsync("SpyStoreAuth", principal);
            return RedirectToLocal(
                returnUrl?.Replace("-1",customer.Id.ToString()),method);
        }

        //private IList<Claim> GetClaims(Customer customer) => new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name, customer.FullName)
        //    , new Claim(ClaimTypes.Email, customer.EmailAddress)
        //    , new Claim("Id", customer.Id.ToString())
        //};

        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl, string method)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            Response.Cookies.Delete("CustomerId",
                new CookieOptions() {Expires=DateTime.Now.AddDays(-1)});
            Response.Cookies.Delete("CustomerName", 
                new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
