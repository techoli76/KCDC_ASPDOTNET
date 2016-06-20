using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        public SearchController([FromServices]IProductRepo repo)
        {
            Repo = repo;
        }

        public IProductRepo Repo { get; set; }

        [HttpGet("{searchString}", Name = "SearchProducts")]
        public IEnumerable<ProductWithCategoryName> GetAll(string searchString) => Repo.Search(searchString);
        //pursuade%20anyone
    }
}
