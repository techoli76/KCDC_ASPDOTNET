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
    public class OrderRepoTests
    {
        private readonly OrderRepo _repo;

        public OrderRepoTests()
        {
            _repo = new OrderRepo(new OrderDetailRepo());
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Fact]
        public void ShouldGetAllOrders()
        {
            var orders = _repo.GetAll();
            Assert.Equal(1,orders.Count());
        }
    }
}
