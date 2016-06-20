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
    public class OrderDetailRepoTests
    {
        private readonly OrderDetailRepo _repo;

        public OrderDetailRepoTests()
        {
            _repo = new OrderDetailRepo();
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Fact]
        public void ShouldGetAllOrderDetails()
        {
            var orders = _repo.GetAll();
            Assert.Equal(3,orders.Count());
        }

        [Fact]
        public void ShouldGetLineItemTotal()
        {
            var orderDetails = _repo.GetAll();
            var orderDetail = orderDetails.ToList()[0];
            Assert.Equal(1799.9700M, orderDetail.LineItemTotal);
        }

    }
}
