using System.Collections.Generic;
using SpyStore.DAL.Repos.Base;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos.Interfaces
{
    public interface IProductRepo : IRepo<Product>
    {
        IEnumerable<ProductWithCategoryName> Search(string searchString);
        IEnumerable<ProductWithCategoryName> GetAllWithCategoryName();
        IEnumerable<Product> GetProductsForCategory(int id);
        IEnumerable<ProductWithCategoryName> GetFeaturedWithCategoryName();
        ProductWithCategoryName GetOneWithCategoryName(int id);
    }
}
