using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class ShoppingCartController : Controller
    {
        public ShoppingCartController([FromServices]IShoppingCartRepo repo)
        {
            Repo = repo;
        }

        public IShoppingCartRepo Repo { get; set; }

        [HttpGet("{productId}",Name = "GetShoppingCartRecord")]
        public CartRecordWithProductInfo GetOne(int customerId, int productId) => Repo.GetShoppingCartRecord(customerId, productId);

        [HttpGet(Name = "GetShoppingCart")]
        public IEnumerable<CartRecordWithProductInfo> GetAll(int customerId) => Repo.GetShoppingCartRecords(customerId);


        [HttpPost] //required even if method name starts with "Post"
        public IActionResult Create(int customerId, [FromBody] ShoppingCartRecord item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            item.DateCreated = DateTime.Now;
            item.CustomerId = customerId;
            Repo.Add(item);
            return CreatedAtRoute("GetShoppingCart",
                new { controller = "ShoppingCart", customerId = customerId }, item);
        }

        [HttpPut("{shoppingCartRecordId}")] //Required even if method name starts with Put
        public IActionResult Update(int customerId, int shoppingCartRecordId, [FromBody] ShoppingCartRecord item)
        {
            if (item == null || item.Id != shoppingCartRecordId)
            {
                return BadRequest();
            }
            try
            {
                item.DateCreated = DateTime.Now;
                Repo.Update(item);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
            return CreatedAtRoute("GetShoppingCart",
                new { controller = "ShoppingCart", customerId = customerId }, item);
            //return new NoContentResult();
        }

        [HttpDelete("{id}/{timeStamp}")] //Required even if method name starts with Delete
        public IActionResult Delete(int customerId, int id, string timeStamp)
        {
            var ts = JsonConvert.DeserializeObject<byte[]>(timeStamp);
            try
            {
                Repo.Delete(id, ts);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var fooEx = ex;
                //var foo = ex.Entries[0]?.Property("Foo")?.CurrentValue;
                //var bar = ex.Entries[0]?.Property("Foo")?.OriginalValue;
                //This should be handled appropriately.  In our case, we are just swallowing it.
            }
            return new NoContentResult();
        }
        [HttpPost("buy")] //required even if method name starts with "Post"
        public IActionResult Purchase(int customerId, [FromBody] Customer customer)
        {

            if (customer == null || customer.Id != customerId)
            {
                //TODO: check addition fields?
                return BadRequest();
            }
            var orderId = Repo.Purchase(customerId);
            return CreatedAtRoute("GetOrderDetails", routeValues: new { controller = "Orders", customerId = customerId, orderId = orderId },
                value: orderId);

        }

    }
}
