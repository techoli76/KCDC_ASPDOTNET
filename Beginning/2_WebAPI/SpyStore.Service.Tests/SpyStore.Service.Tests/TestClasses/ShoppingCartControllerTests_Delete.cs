using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SpyStore.DAL.EF;
using SpyStore.DAL.EF.Initializers;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    public partial class ShoppingCartControllerTests : IDisposable
    {
        [Fact]
        public async void ShouldDeleteRecordInTheCart()
        {
            // Remove Cart Item: http://localhost:40001/api/shoppingcart/{customerId}/{id}/{TimeStamp} HTTPDelete
            // http://localhost:40001/api/shoppingcart/1/2/AAAAAAAA1Uc=
            var cartRecord = await GetCartItem(2, 34);
            var timeStampString = JsonConvert.SerializeObject(cartRecord.TimeStamp);
            using (var client = new HttpClient())
            {
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2/2/{timeStampString}";
                var response = await client.DeleteAsync(targetUrl);
                Assert.True(response.IsSuccessStatusCode);
            }
            // validate the cart item was updated
            var cart = await GetCart(2);
            Assert.Equal(0, cart.Count);
        }

        [Fact]
        public async void ShouldNotDeleteMissingRecord()
        {
            // Remove Cart Item: http://localhost:40001/api/shoppingcart/{customerId}/{id}/{TimeStamp} HTTPDelete
            // http://localhost:40001/api/shoppingcart/1/2/AAAAAAAA1Uc=
            var cartRecord = await GetCartItem(2, 34);
            var timeStampString = JsonConvert.SerializeObject(cartRecord.TimeStamp);
            using (var client = new HttpClient())
            {
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2/4/{timeStampString}";
                var response = await client.DeleteAsync(targetUrl);
                Assert.True(response.IsSuccessStatusCode);
            }
            // validate the cart item was updated
            var cart = await GetCart(2);
            Assert.Equal(1, cart.Count);
        }

    }
}
