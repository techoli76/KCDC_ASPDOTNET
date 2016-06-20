using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;
using SpyStore.MVC.Models.ViewModels;

namespace SpyStore.MVC.Controllers
{
    public class HomeController : SpyStoreBaseController
    {

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewBag.ShowCategory = true;
            ViewBag.Header = "Featured Products";
            ViewBag.ReturnUrl = Request.Path + Request.QueryString;
            //ViewBag.ReturnUrl = Url.Action("Index", "Home");
            var prods = await WebAPICalls.GetFeaturedProducts();
            if (prods == null) return NotFound();
            //return View("IndexAjax",prods);
            return View(prods);
        }

        [HttpGet]
        public async Task<ActionResult> IndexViewBag()
        {
            ViewBag.Categories = await WebAPICalls.GetCategories();
            var prods = await WebAPICalls.GetFeaturedProducts();
            if (prods != null)
            {
                //return View("IndexAjax",prods);
                return View(prods);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> IndexPartial()
        {
            var viewModel = new IndexPageViewModel
            {
                Products = await WebAPICalls.GetFeaturedProducts(),
                Categories = await WebAPICalls.GetCategories()
            };
            return View(viewModel);
        }

        public Task<IActionResult> Products(int id)
        {
            return null;

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
