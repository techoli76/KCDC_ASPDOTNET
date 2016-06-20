using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;

namespace SpyStore.MVC.Controllers
{
    public class CategoryController : SpyStoreBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Menu()
        {
            var cats = await WebAPICalls.GetCategories();
            if (cats == null) return NotFound();
            return PartialView(cats);
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(int id)
        {
            ViewBag.ReturnUrl = Request.Path + Request.QueryString;
            var cat = await WebAPICalls.GetCategory(id);
            ViewBag.Title = cat?.CategoryName;
            ViewBag.Header = cat?.CategoryName;
            ViewBag.ShowCategory = false;
            var prods = await WebAPICalls.GetProductsForACategory(id);
            if (prods != null)
            {
                return View("ProductList",prods);
            }
            return NotFound();

        }
    }
}
