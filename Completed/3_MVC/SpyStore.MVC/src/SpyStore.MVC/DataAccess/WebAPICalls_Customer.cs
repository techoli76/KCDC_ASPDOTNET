using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpyStore.Models.Entities;

namespace SpyStore.MVC.DataAccess
{
    public static partial class WebAPICalls
    {
        internal static async Task<IList<Customer>> GetCustomers()
        {
            //Get All Customers: http://localhost:33816/api/customer
            try
            {
                using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ServiceAddress}api/customer");
                if (!response.IsSuccessStatusCode) return null;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<Customer>>(jsonResponse);
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        internal static async Task<Customer> GetCustomer(int id)
        {
            //Get One customer: http://localhost:33816/api/customer/{id}
            //http://localhost:33816/api/customer/1
            try
            {
                using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ServiceAddress}api/customer/{id}");
                if (!response.IsSuccessStatusCode) return null;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(jsonResponse);
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
