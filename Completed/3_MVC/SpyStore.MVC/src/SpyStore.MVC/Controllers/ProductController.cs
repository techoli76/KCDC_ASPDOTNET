using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpyStore.Models.ViewModels;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;
using SpyStore.MVC.Models.ViewModels;

namespace SpyStore.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : SpyStoreBaseController
    {
        MapperConfiguration _config;
        public ProductController()
        {
            _config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ProductWithCategoryName, AddToCartViewModel>();
                });
        }

        [HttpGet]
        public async Task<ActionResult> Featured()
        {
            ViewBag.ReturnUrl = Request.Path + Request.QueryString;
            ViewBag.Title = "Featured Products";
            ViewBag.Header = "Featured Products";
            ViewBag.ShowCategory = true;
            var prods = await WebAPICalls.GetFeaturedProducts();
            if (prods == null) return NotFound();
            return View("ProductList",prods);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.CartReturnUrl = Request.Path;// + Request.QueryString;
            ViewBag.Title = "Product Details";
            ViewBag.Header = "Featured Products";
            ViewBag.ShowCategory = false;
            var prod = await WebAPICalls.GetOneProduct(id);
            if (prod == null) return NotFound();
            var mapper = _config.CreateMapper();
            var cartRecord = mapper.Map<AddToCartViewModel>(prod);
            return View(cartRecord);
        }

        [HttpPost("{searchString}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchString)
        {
            ViewBag.ReturnUrl = Request.Path + Request.QueryString;
            ViewBag.Title = "Search Results";
            ViewBag.Header = "Search Results";
            ViewBag.ShowCategory = true;
            var prods = await WebAPICalls.Search(searchString);
            if (prods != null)
            {
                return View("ProductList", prods);
            }
            return NotFound();

        }


    }
}
