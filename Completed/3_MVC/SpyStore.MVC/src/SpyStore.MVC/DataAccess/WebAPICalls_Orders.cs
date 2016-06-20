using System;
using System.Collections.Generic;
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


        internal static async Task<IList<OrderWithTotal>> GetOrders(int customerId)
        {
            //Get Order History: http://localhost:33816/api/orders/{customerId}
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/orders/{customerId}");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<OrderWithTotal>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        internal static async Task<OrderWithTotal> GetOrderDetails(int customerId, int orderId)
        {
            //Get Order Details: http://localhost:33816/api/orders/{customerId}/{orderId}
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{ServiceAddress}api/orders/{customerId}/{orderId}");
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OrderWithTotal>(jsonResponse);
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
