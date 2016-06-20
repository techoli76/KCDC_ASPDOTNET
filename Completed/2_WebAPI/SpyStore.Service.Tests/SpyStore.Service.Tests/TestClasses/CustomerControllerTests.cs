using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SpyStore.Models.Entities;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public class CustomerControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/customer";
        public CustomerControllerTests()
        {
        }
        public void Dispose()
        {
        }

        [Fact]
        public async void ShouldGetAllCustomers()
        {
            //Get All Customers: http://localhost:40001/api/customer
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse);
                Assert.Equal(1,customers.Count);
            }
        }

        [Fact]
        public async void ShouldGetOneCustomer()
        {
            //Get One customer: http://localhost:40001/api/customer/2
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/2");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(jsonResponse);
                Assert.Equal("Super Spy",customer.FullName);
            }
        }

        [Fact]
        public async void ShouldFailIfBadCustomerId()
        {
            //Get One Category: http://localhost:40001/api/customer/1
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/1");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            }
        }
    }
}
