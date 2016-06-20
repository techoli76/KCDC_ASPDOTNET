using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class OrdersController : Controller
    {
        public OrdersController([FromServices]IOrderRepo repo)
        {
            Repo = repo;
        }

        public IOrderRepo Repo { get; set; }

        [HttpGet(Name = "GetCustomerOrderHistory")]
        public IActionResult GetOrderHistory(int customerId)
        {
            //TODO: Change this in the DAL to return null?
            var orderWithTotals = Repo.GetAllWithTotal(customerId);
            return orderWithTotals == null ? (IActionResult)NotFound() : new ObjectResult(orderWithTotals);
        }

        [HttpGet("{orderId}",Name = "GetOrderDetails")]
        public IActionResult GetOne(int customerId,int orderId)
        {
            //TODO: Change this in the DAL to return null?
            var orderWithDetails = Repo.GetOneWithDetails(customerId, orderId);
            return orderWithDetails == null ? (IActionResult) NotFound() : new ObjectResult(orderWithDetails);
        }
    }
}
