using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public ProductController([FromServices]IProductRepo repo)
        {
            Repo = repo;
        }

        public IProductRepo Repo { get; set; }

        [HttpGet]
        public IEnumerable<ProductWithCategoryName> GetAll() 
            => Repo.GetAllWithCategoryName();

        [HttpGet("featured")]
        public IEnumerable<ProductWithCategoryName> GetFeatured() 
            => Repo.GetFeaturedWithCategoryName();

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var item = Repo.GetOneWithCategoryName(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

    }
}
