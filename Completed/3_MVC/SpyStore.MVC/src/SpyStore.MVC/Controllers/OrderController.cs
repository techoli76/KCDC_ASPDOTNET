using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;

namespace SpyStore.MVC.Controllers
{
    [Route("[controller]/[action]/{customerId}")]
    public class OrderController : SpyStoreBaseController
    {
        
        [HttpGet]
        public async Task<IActionResult> Index(int customerId)
        {
            ViewBag.Title = "Order History";
            ViewBag.Header = "Order History";
            var orders = await WebAPICalls.GetOrders(customerId);
            if (orders == null) return NotFound();
            return View(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Details(int customerId, int orderId)
        {
            ViewBag.Title = "Order Details";
            ViewBag.Header = "Order Details";
            var orderDetails = await WebAPICalls.GetOrderDetails(customerId, orderId);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

    }
}
