using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF.Initializers;
using SpyStore.DAL.Repos;
using SpyStore.Models.Entities;
using Xunit;

namespace SpyStore.DAL.Tests
{
    [Collection("Database Testing")]
    public class ShoppingCartRepoTests
    {
        private readonly ShoppingCartRepo _repo;

        public ShoppingCartRepoTests()
        {
            _repo = new ShoppingCartRepo();
            StoreDataInitializer.InitializeData(_repo.Context);

        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }

        [Fact]
        public void ShouldAddAnItemToTheCart()
        {
            var item = new ShoppingCartRecord();
            item.ProductId = 2;
            item.Quantity = 5;
            item.DateCreated = DateTime.Now;
            item.CustomerId = 2;
            _repo.Add(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(2,shoppingCartRecords.Count);
            Assert.Equal(2,shoppingCartRecords[1].ProductId);
            Assert.Equal(5,shoppingCartRecords[1].Quantity);
        }
        [Fact]
        public void ShouldUpdateQuanityOnAddIfAlreadyInCart()
        {
            var item = new ShoppingCartRecord();
            item.ProductId = 34;
            item.Quantity = 1;
            item.DateCreated = DateTime.Now;
            item.CustomerId = 2;
            _repo.Add(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(1,shoppingCartRecords.Count);
            Assert.Equal(2,shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuanityEqualZero()
        {
            var item = new ShoppingCartRecord();
            item.ProductId = 34;
            item.Quantity = -1;
            item.DateCreated = DateTime.Now;
            item.CustomerId = 2;
            _repo.Add(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(0,shoppingCartRecords.Count);
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuanityLessThanZero()
        {
            var item = new ShoppingCartRecord();
            item.ProductId = 34;
            item.Quantity = -10;
            item.DateCreated = DateTime.Now;
            item.CustomerId = 2;
            _repo.Add(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(0,shoppingCartRecords.Count);
        }
        [Fact]
        public void ShouldUpdateQuanity()
        {
            var item = _repo.Find(2, 34);
            item.Quantity = 5;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(1,shoppingCartRecords.Count);
            Assert.Equal(5, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuanityEqualsZero()
        {
            var item = _repo.Find(2, 34);
            item.Quantity = 0;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(0, shoppingCartRecords.Count);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuanityLessThanZero()
        {
            var item = _repo.Find(2, 34);
            item.Quantity = -10;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var records = _repo.GetAll();
            var shoppingCartRecords = records.ToList();
            Assert.Equal(0, shoppingCartRecords.Count);
        }

        [Fact]
        public void ShouldDeleteCartRecord()
        {
            var item = _repo.Find(2, 34);
            _repo.Context.Entry(item).State = EntityState.Detached;
            _repo.Delete(item.Id, item.TimeStamp);
            Assert.Equal(0, _repo.GetAll().Count());
        }
        [Fact]
        public void ShouldNotDeleteMissingCartRecord()
        {
            var item = _repo.Find(2, 34);
            Assert.Throws<DbUpdateConcurrencyException>(()=>_repo.Delete(200, item.TimeStamp));
        }
    }
}
