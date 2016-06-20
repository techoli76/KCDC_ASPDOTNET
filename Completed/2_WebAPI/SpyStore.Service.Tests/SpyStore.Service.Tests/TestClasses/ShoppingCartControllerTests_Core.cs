using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using SpyStore.DAL.EF;
using SpyStore.DAL.EF.Initializers;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public partial class ShoppingCartControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/shoppingcart";
        //private string _customerId = "/2";

        private async Task<List<CartRecordWithProductInfo>> GetCart(int customerId)
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{_serviceAddress}{_rootAddress}/{customerId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CartRecordWithProductInfo>>(jsonResponse);
            }
        }

        private async Task<CartRecordWithProductInfo> GetCartItemWithProduct(
            int customerId, int productId)
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{_serviceAddress}{_rootAddress}/{customerId}/{productId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CartRecordWithProductInfo>(jsonResponse);
            }

        }
        private async Task<ShoppingCartRecord> GetCartItem(
            int customerId, int productId)
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{_serviceAddress}{_rootAddress}/{customerId}/{productId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var fullCartObject = JsonConvert.DeserializeObject<CartRecordWithProductInfo>(jsonResponse);
                return Mapper.Map<CartRecordWithProductInfo,ShoppingCartRecord>(fullCartObject);

            }
        }

        public ShoppingCartControllerTests()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<CartRecordWithProductInfo, ShoppingCartRecord>();
                });

            StoreDataInitializer.InitializeData(new SpyStoreContext());
        }

        public void Dispose()
        {
            StoreDataInitializer.InitializeData(new SpyStoreContext());
        }

        [Fact]
        public async void ShouldReturnCustomersCart()
        {
            //Get Cart: http://localhost:40001/api/shoppingcart/{customerId}
            var cartRecordsWithProductDetails = await GetCart(2);
            Assert.Equal(1, cartRecordsWithProductDetails.Count);
            Assert.Equal(34, cartRecordsWithProductDetails[0].ProductId);
            Assert.Equal("Travel", cartRecordsWithProductDetails[0].CategoryName);
        }

        [Fact]
        public async void ShouldNotFailIfBadCustomerId()
        {
            //Get Cart: http://localhost:40001/api/shoppingcart/{customerId}
            var cartRecordsWithProductDetails = await GetCart(0);
            Assert.Equal(0, cartRecordsWithProductDetails.Count);
        }

        [Fact]
        public async void ShouldReturnCustomersCartItem()
        {
            //Get Cart: http://localhost:40001/api/shoppingcart/{customerId}
            var cartRecordWithProductDetails = await GetCartItemWithProduct(2,34);
            Assert.Equal(34, cartRecordWithProductDetails.ProductId);
            Assert.Equal(1, cartRecordWithProductDetails.Quantity);
            Assert.Equal("Travel", cartRecordWithProductDetails.CategoryName);
        }
    }
}
