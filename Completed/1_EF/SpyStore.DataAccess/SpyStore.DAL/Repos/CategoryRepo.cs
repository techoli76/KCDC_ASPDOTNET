using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;

namespace SpyStore.DAL.Repos
{
    public class CategoryRepo : RepoBase<Category>, ICategoryRepo
    {

        public CategoryRepo(DbContextOptions<SpyStoreContext> options) : base(options)
        {
            Table = Context.Categories;
        }
        public CategoryRepo()
        {
            Table = Context.Categories;
        }

        public override IEnumerable<Category> GetAll() =>
            Table.OrderBy(x => x.CategoryName).ToList();

        public Category GetOneWithProducts(int? id) =>
            Table.Include(x => x.Products).SingleOrDefault(x => x.Id == id);

        public IEnumerable<Category> GetAllWithProducts() =>
            Table.Include(x => x.Products).ToList();
    }
}
