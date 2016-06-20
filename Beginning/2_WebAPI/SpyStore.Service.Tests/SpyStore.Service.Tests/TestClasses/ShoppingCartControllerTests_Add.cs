using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SpyStore.DAL.EF;
using SpyStore.DAL.EF.Initializers;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    public partial class ShoppingCartControllerTests
    {

        [Fact]
        public async void ShouldAddRecordToTheCart()
        {
            // Add to Cart: http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            // Note: ProductId and Quantity in the body
            // http://localhost:40001/api/shoppingcart/1 {"ProductId":22,"Quantity":2}
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var productId = 2;
                var quantity = 5;
                //var request = new HttpRequestMessage(
                //    HttpMethod.Post,
                //    $"{_serviceAddress}{_rootAddress}{_customerId}")
                //{
                //    Content = new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}", Encoding.UTF8, "application/json")
                //};
                //var response = await client.SendAsync(request);
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2";
                var response = await client.PostAsync(targetUrl,
                    new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}",
                        Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal(targetUrl.ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cartRecordsWithProductDetails = await GetCart(2);
            Assert.Equal(2, cartRecordsWithProductDetails.Count);
            var cartRecord = cartRecordsWithProductDetails[0];
            Assert.Equal(2, cartRecord.ProductId);
            Assert.Equal("Communications", cartRecord.CategoryName);
            Assert.Equal(5, cartRecord.Quantity);
        }

        [Fact]
        public async void ShouldUpdateCartRecordOnAddIfAlreadyExists()
        {
            // Add to Cart: http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            // Note: ProductId and Quantity in the body {"ProductId":22,"Quantity":2}
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var productId = 34;
                var quantity = 5;
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2";
                var response = await client.PostAsync(targetUrl,
                    new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}",
                        Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal(targetUrl.ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cartRecordsWithProductDetails = await GetCart(2);
            Assert.Equal(1, cartRecordsWithProductDetails.Count);
            var cartRecord = cartRecordsWithProductDetails[0];
            Assert.Equal(34, cartRecord.ProductId);
            Assert.Equal("Travel", cartRecord.CategoryName);
            Assert.Equal(6, cartRecord.Quantity);
        }
        [Fact]
        public async void ShouldRemoveRecordOnAddIfQuantityBecomesZero()
        {
            // Add to Cart: http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            // Note: ProductId and Quantity in the body {"ProductId":22,"Quantity":2}
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var productId = 34;
                var quantity = -1;
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2";
                var response = await client.PostAsync(targetUrl,
                    new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}",
                        Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal(targetUrl.ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cartRecordsWithProductDetails = await GetCart(2);
            Assert.Equal(0, cartRecordsWithProductDetails.Count);
        }
        [Fact]
        public async void ShouldRemoveRecordOnAddIfQuantityBecomesLessThanZero()
        {
            // Add to Cart: http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            // Note: ProductId and Quantity in the body {"ProductId":22,"Quantity":2}
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var productId = 34;
                var quantity = -10;
                var targetUrl = $"{_serviceAddress}{_rootAddress}/2";
                var response = await client.PostAsync(targetUrl,
                    new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}",
                        Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal(targetUrl.ToUpper(),
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            // validate the cart was added
            var cartRecordsWithProductDetails = await GetCart(2);
            Assert.Equal(0, cartRecordsWithProductDetails.Count);
        }
    }
}
