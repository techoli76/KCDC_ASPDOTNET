using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
    public class ProductRepo : RepoBase<Product>, IProductRepo
    {
        public ProductRepo(DbContextOptions<SpyStoreContext> options) : base(options)
        {
            Table = Context.Products;
        }
        public ProductRepo() : base()
        {
            Table = Context.Products;
        }
        public override IEnumerable<Product> GetAll() =>
            Table.OrderBy(x => x.ModelName);

        public IEnumerable<Product> GetProductsForCategory(int id)
            => Table
            .Where(p => p.CategoryId == id)
            .OrderBy(x => x.ModelName).ToList();

        public IEnumerable<ProductWithCategoryName> GetAllWithCategoryName()
            => Table
                .Include(p => p.Category)
                .Select(item => new ProductWithCategoryName()
                {
                    CategoryName = item.Category.CategoryName,
                    CategoryId = item.CategoryId,
                    CurrentPrice = item.CurrentPrice,
                    Description = item.Description,
                    IsFeatured = item.IsFeatured,
                    Id = item.Id,
                    ModelName = item.ModelName,
                    ModelNumber = item.ModelNumber,
                    ProductImage = item.ProductImage,
                    ProductImageLarge = item.ProductImageLarge,
                    ProductImageThumb = item.ProductImageThumb,
                    TimeStamp = item.TimeStamp,
                    UnitCost = item.UnitCost,
                    UnitsInStock = item.UnitsInStock
                })
                .OrderBy(x => x.ModelName);

        public IEnumerable<ProductWithCategoryName> GetFeaturedWithCategoryName()
            => Table
            .Where(p => p.IsFeatured)
            .Include(p => p.Category)
            .Select(item => new ProductWithCategoryName()
            {
                CategoryName = item.Category.CategoryName,
                CategoryId = item.CategoryId,
                CurrentPrice = item.CurrentPrice,
                Description = item.Description,
                IsFeatured = item.IsFeatured,
                Id = item.Id,
                ModelName = item.ModelName,
                ModelNumber = item.ModelNumber,
                ProductImage = item.ProductImage,
                ProductImageLarge = item.ProductImageLarge,
                ProductImageThumb = item.ProductImageThumb,
                TimeStamp = item.TimeStamp,
                UnitCost = item.UnitCost,
                UnitsInStock = item.UnitsInStock
            })
            .OrderBy(x => x.ModelName);

        public ProductWithCategoryName GetOneWithCategoryName(int id)
        =>
            Table
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Select(item => new ProductWithCategoryName()
                {
                    CategoryName = item.Category.CategoryName,
                    CategoryId = item.CategoryId,
                    CurrentPrice = item.CurrentPrice,
                    Description = item.Description,
                    IsFeatured = item.IsFeatured,
                    Id = item.Id,
                    ModelName = item.ModelName,
                    ModelNumber = item.ModelNumber,
                    ProductImage = item.ProductImage,
                    ProductImageLarge = item.ProductImageLarge,
                    ProductImageThumb = item.ProductImageThumb,
                    TimeStamp = item.TimeStamp,
                    UnitCost = item.UnitCost,
                    UnitsInStock = item.UnitsInStock
                })
                .SingleOrDefault();

        public IEnumerable<ProductWithCategoryName> Search(string searchString)
            => Table
            .Where(p =>
                p.Description.ToLower().Contains(searchString.ToLower())
                || p.ModelName.ToLower().Contains(searchString.ToLower()))
            .Include(p => p.Category)
            .Select(item => new ProductWithCategoryName()
            {
                CategoryName = item.Category.CategoryName,
                CategoryId = item.CategoryId,
                CurrentPrice = item.CurrentPrice,
                Description = item.Description,
                IsFeatured = item.IsFeatured,
                Id = item.Id,
                ModelName = item.ModelName,
                ModelNumber = item.ModelNumber,
                ProductImage = item.ProductImage,
                ProductImageLarge = item.ProductImageLarge,
                ProductImageThumb = item.ProductImageThumb,
                TimeStamp = item.TimeStamp,
                UnitCost = item.UnitCost,
                UnitsInStock = item.UnitsInStock
            })
            .OrderBy(x => x.ModelName);
    }
}
