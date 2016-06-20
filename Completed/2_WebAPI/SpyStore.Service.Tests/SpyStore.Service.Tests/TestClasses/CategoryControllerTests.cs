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
    public class CategoryControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/category";
        public CategoryControllerTests()
        {
        }
        public void Dispose()
        {
        }

        [Fact]
        public async void ShouldGetAllCategories()
        {
            //Get All Categories: http://localhost:40001/api/category
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cats = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
                Assert.Equal(7,cats.Count);
            }
        }
        [Theory]
        [InlineData(2,"Communications")]
        [InlineData(3,"Deception")]
        [InlineData(4,"Travel")]
        [InlineData(5,"Protection")]
        [InlineData(6,"Munitions")]
        [InlineData(7,"Tools")]
        [InlineData(8,"General")]
        public async void ShouldGetOneCategory(int catId, string categoryName)
        {
            //Get One Category: http://localhost:40001/api/category/1
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/{catId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cat = JsonConvert.DeserializeObject<Category>(jsonResponse);
                Assert.Equal(categoryName,cat.CategoryName);
            }
        }

        [Fact]
        public async void ShouldFailIfBadCategoryId()
        {
            //Get One Category: http://localhost:40001/api/category/1
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/0");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            }
        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        [InlineData(4, 6)]
        [InlineData(5, 6)]
        [InlineData(6, 3)]
        [InlineData(7, 7)]
        [InlineData(8, 9)]
        public async void ShouldGetProductsForACategory(int catId, int productCount)
        {
            //Get Products For Category: http://localhost:40001/api/category/{id}/products
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/{catId}/products");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var prods = JsonConvert.DeserializeObject<IList<Product>>(jsonResponse);
                Assert.Equal(productCount,prods.Count);

            }
        }
    }
}
