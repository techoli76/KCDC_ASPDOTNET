using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpyStore.DAL.EF.Initializers;
using SpyStore.DAL.Repos;
using SpyStore.Models.Entities;
using Xunit;

namespace SpyStore.DAL.Tests
{
    [Collection("Database Testing")]
    public class ProductRepoTests
    {
        private readonly ProductRepo _repo;

        public ProductRepoTests()
        {
            _repo = new ProductRepo();
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        [InlineData(4, 6)]
        [InlineData(5, 6)]
        [InlineData(6, 3)]
        [InlineData(7, 7)]
        [InlineData(8, 9)]
        public void ShouldGetAllProductsForACategory(int catId, int productCount)
        {
            var prods = _repo.GetProductsForCategory(catId);
            Assert.Equal(productCount,prods.Count());
        }
    }
}
