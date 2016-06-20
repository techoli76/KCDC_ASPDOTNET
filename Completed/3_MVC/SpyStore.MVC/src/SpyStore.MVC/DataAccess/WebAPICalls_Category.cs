using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpyStore.Models.Entities;

namespace SpyStore.MVC.DataAccess
{
    public static partial class WebAPICalls
    {
        //http://blogs.msdn.com/b/martinkearn/archive/2015/06/17/using-httpclient-with-asp-net-5-0.aspx

        internal static async Task<IList<Category>> GetCategories()
        {
            //http://localhost:33816/api/category
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/category");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        internal static async Task<Category> GetCategory(int id)
        {
            //http://localhost:33816/api/category/{id}
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/category/{id}");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Category>(jsonResponse);
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
