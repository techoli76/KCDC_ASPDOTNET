using System;
using System.Collections.Generic;
using System.Linq;
using SpyStore.DAL.Repos;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SpyStore.Models.Entities;

namespace SpyStore.DAL.Tests
{
    [Collection("Database Testing")]
    public class CategoryRepoTests : IDisposable
    {
        private readonly CategoryRepo _repo;

        public CategoryRepoTests()
        {
            _repo = new CategoryRepo();
            DatabaseUtilities.CleanDataBase(_repo.Context, _repo.GetTableName());
        }
        public void Dispose()
        {
            DatabaseUtilities.CleanDataBase(_repo.Context, _repo.GetTableName());
            _repo.Dispose();
        }

        private Category CreateCategoryInstance(string categoryName)
        {
            return new Category { CategoryName = categoryName, Products = CreateProducts() };
        }

        private IList<Product> CreateProducts()
        {
            var prods = new List<Product>
            {
                new Product() {CurrentPrice = 12.99M, ModelName = "Product 1", ModelNumber = "P1"},
                new Product() {CurrentPrice = 9.99M, ModelName = "Product 2", ModelNumber = "P2"},
            };
            return prods;
        }

        [Fact]
        public void ShouldAddACategory()
        {
            var category = CreateCategoryInstance("Foo");
            _repo.Add(category);
            Assert.Equal(2, category.Id);
            Assert.Equal(1, _repo.GetAll().Count());
        }

        [Fact]
        public void ShouldGetACategoryWithProductInfo()
        {
            var category = CreateCategoryInstance("Foo");
            _repo.Add(category);
            var cat = _repo.GetOneWithProducts(2);
            Assert.Equal(2, cat.Products.Count());
        }

        [Fact]
        public void ShouldGetCategoryWithGetOne()
        {
            var category = CreateCategoryInstance("Foo");
            _repo.Add(category);
            _repo.GetOne(category.Id);
        }

        [Fact]
        public void ShouldGetCategoryWithFind()
        {
            var category = CreateCategoryInstance("Foo");
            _repo.Add(category);
            _repo.Find(category.Id);
        }

    }
}
