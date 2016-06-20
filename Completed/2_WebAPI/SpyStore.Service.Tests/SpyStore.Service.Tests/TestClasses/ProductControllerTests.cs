using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public class ProductControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/product";
        public ProductControllerTests()
        {
        }
        public void Dispose()
        {
        }

        [Fact]
        public async void ShouldGetAllProductsWithCategoryName()
        {
            //Get All Products With Category Name: http://localhost:40001/api/product
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryNames = JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                Assert.Equal(41,productWithCategoryNames.Count);
                Assert.Equal("Protection",productWithCategoryNames[0].CategoryName);
            }
        }

        [Fact]
        public async void ShouldGetOneProductWithCategoryName()
        {
            //Get Featured Products With Category Name: http://localhost:40001/api/product/featured
            //Get One Product With Category Name: http://localhost:40001/api/product/2
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/2");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryName = JsonConvert.DeserializeObject<ProductWithCategoryName>(jsonResponse);
                Assert.Equal("Communications Device",productWithCategoryName.ModelName);
                Assert.Equal("Communications",productWithCategoryName.CategoryName);
            }
        }

        [Fact]
        public async void ShouldFailIfBadCustomerId()
        {
            //Get One Product with Category Name: http://localhost:40001/api/product/1
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/0");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            }
        }
    }
}
