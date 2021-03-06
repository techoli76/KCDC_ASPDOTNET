﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SpyStore.Models.ViewModels;
using Xunit;

namespace SpyStore.Service.Tests.TestClasses
{
    [Collection("Service Testing")]
    public class SearchControllerTests : IDisposable
    {
        private string _serviceAddress = "http://localhost:40001/";
        private string _rootAddress = "api/search";
        public SearchControllerTests()
        {
        }
        public void Dispose()
        {
        }

        [Fact]
        public async void ShouldReturnProductsWithCategoryName()
        {
            //Search Products: http://localhost:40001/api/search/{searchString} 
           // Note: encode spaces with % 20
        //http://localhost:40001/api/search/persuade%20anyone

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/persuade%20anyone");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryNames = JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                Assert.Equal(1,productWithCategoryNames.Count);
                Assert.Equal("Communications",productWithCategoryNames[0].CategoryName);
            }
        }

        [Fact]
        public async void ShouldNotFailIfBadSearchId()
        {
            //Get Products with Category Name: http://localhost:40001/api/search/persuade%20anyone
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_serviceAddress}{_rootAddress}/FooBar");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productWithCategoryNames = JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                Assert.Equal(0, productWithCategoryNames.Count);
            }
        }
    }
}
