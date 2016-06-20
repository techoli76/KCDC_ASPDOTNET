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
        public async void ShouldUpdateRecordInTheCart()
        {
            //Change Cart Item(Quantity): http://localhost:40001/api/shoppingcart/{customerId}/{productId} HTTPPut
            //Note: Id, CustomerId, ProductId, TimeStamp, DateCreated, & Quantity in  body
            //{ "Id":1,"CustomerId":1,"ProductId":33,"Quantity":2, "TimeStamp":"AAAAAAAA86s=", "DateCreated":"1/20/2016"}
            var cartRecord = await GetCartItem(2, 34);
            cartRecord.Quantity = 5;
            var json = JsonConvert.SerializeObject(cartRecord);
            using (var client = new HttpClient())
            {
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2/2";
                var response = await client.PutAsync(targetUrl,
                    new StringContent(json,Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{_serviceAddress}{_rootAddress}/2".ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart item was updated
            var updatedCartRecord = await GetCartItem(2, 34);
            Assert.Equal(5, updatedCartRecord.Quantity);
        }

        [Fact]
        public async void ShouldRemoveRecordOnUpdateIfQuantityBecomesZero()
        {
            var cartRecord = await GetCartItem(2, 34);
            cartRecord.Quantity = 0;
            var json = JsonConvert.SerializeObject(cartRecord);
            using (var client = new HttpClient())
            {
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2/2";
                var response = await client.PutAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{_serviceAddress}{_rootAddress}/2".ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cart = await GetCart(2);
            Assert.Equal(0, cart.Count);
        }

        [Fact]
        public async void ShouldRemoveRecordOnUpdateIfQuantityBecomesLessThanZero()
        {
            var cartRecord = await GetCartItem(2, 34);
            cartRecord.Quantity = -10;
            var json = JsonConvert.SerializeObject(cartRecord);
            using (var client = new HttpClient())
            {
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2/2";
                var response = await client.PutAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{_serviceAddress}{_rootAddress}/2".ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cart = await GetCart(2);
            Assert.Equal(0, cart.Count);
        }
    }
}
