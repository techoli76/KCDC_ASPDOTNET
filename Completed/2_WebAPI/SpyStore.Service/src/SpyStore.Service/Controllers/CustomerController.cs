using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        public CustomerController([FromServices]ICustomerRepo repo)
        {
            Repo = repo;
        }

        public ICustomerRepo Repo { get; set; }

        [HttpGet]
        public IEnumerable<Customer> GetAll() => Repo.GetAll();

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(int id)
        {
            var item = Repo.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}
