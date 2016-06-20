using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        public CategoryController([FromServices]ICategoryRepo repo,[FromServices] IProductRepo productRepo)
        {
            Repo = repo;
            ProductRepo = productRepo;
            //Not needed unless pulling products as well
            //Mapper.Initialize(
            //    cfg =>
            //    {
            //        cfg.CreateMap<Category, Category>();
            //        cfg.CreateMap<Product, Product>()
            //        .ForMember(x => x.Category, opt => opt.Ignore())
            //        .ForMember(x => x.OrderDetails, opt => opt.Ignore())
            //        .ForMember(x => x.Reviews, opt => opt.Ignore())
            //        .ForMember(x => x.ShoppingCartRecords, opt => opt.Ignore());
            //    });
        }

        public ICategoryRepo Repo { get; set; }

        public IProductRepo ProductRepo { get; set; }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            //var categories = Repo.GetAllWithProducts().ToList();
            //var items = Mapper.Map<IList<Category>,IList<Category>>(categories);
            //return items;
            return Repo.GetAll();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(int id)
        {
            //var item = Repo.GetOneWithProducts(id);
            var item = Repo.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            //var itemWithProducts = Mapper.Map<Category, Category>(item);
            //return new ObjectResult(itemWithProducts);
            return new ObjectResult(item);
        }
        [HttpGet("{categoryId}/products", Name = "GetProductsForCategory")]
        public IEnumerable<Product> GetProductsForCategory(int categoryId) => ProductRepo.GetProductsForCategory(categoryId);
    }
}
