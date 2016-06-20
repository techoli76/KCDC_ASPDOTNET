using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpyStore.Models.ViewModels;
using SpyStore.Models.Entities;

namespace SpyStore.MVC.DataAccess
{
    public static partial class WebAPICalls
    {
        //http://blogs.msdn.com/b/martinkearn/archive/2015/06/17/using-httpclient-with-asp-net-5-0.aspx
        internal static async Task<IList<ProductWithCategoryName>> GetFeaturedProducts()
        {
            // http://localhost:33816/api/product/featured
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/product/featured");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        internal static async Task<IList<ProductWithCategoryName>> GetProductsForACategory(int categoryId)
        {
            // http://localhost:33816/api/category/{id}/products
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/category/{categoryId}/products");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        internal static async Task<ProductWithCategoryName> GetOneProduct(int productId)
        {
            // http://localhost:33816/api/product/{id}
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/product/{productId}");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProductWithCategoryName>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        internal static async Task<IList<ProductWithCategoryName>> Search(string searchTerm)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/search/{searchTerm}");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ProductWithCategoryName>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }


        }

    }
}
