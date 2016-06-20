using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public class OrdersControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/orders";
        private string _customerId = "/2";
        public OrdersControllerTests()
        {
        }
        public void Dispose()
        {
        }

        [Fact]
        public async void ShouldGetAllOrdersForACustomer()
        {
            //Get Order History: http://localhost:40001/api/orders/{customerId}
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}{_customerId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ordersWithTotal = JsonConvert.DeserializeObject<List<OrderWithTotal>>(jsonResponse);
                Assert.Equal(1,ordersWithTotal.Count);
                Assert.Equal(4424.90M,ordersWithTotal[0].OrderTotal);
            }
        }

        [Fact]
        public async void ShouldGetOneOrder()
        {
            //Get One Order: http://localhost:40001/api/orders/{customerId}/{orderId}
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}{_customerId}/2");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var orderWithTotal = JsonConvert.DeserializeObject<OrderWithTotal>(jsonResponse);
                Assert.Equal(4424.90M,orderWithTotal.OrderTotal);
                Assert.Equal(3,orderWithTotal.OrderDetails.Count);
            }
        }

        [Fact]
        public async void ShouldFailIfBadOrderId()
        {
            //Get One Order: http://localhost:40001/api/orders/{customerId}/{orderId}
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}{_customerId}/0");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            }
        }
    }
}
