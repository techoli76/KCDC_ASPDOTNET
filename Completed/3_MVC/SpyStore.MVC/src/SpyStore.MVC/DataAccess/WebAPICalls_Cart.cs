using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpyStore.Models.ViewModels;
using SpyStore.Models.Entities;

namespace SpyStore.MVC.DataAccess
{

    public static partial class WebAPICalls
    {
        //http://blogs.msdn.com/b/martinkearn/archive/2015/06/17/using-httpclient-with-asp-net-5-0.aspx

        internal static async Task<IList<CartRecordWithProductInfo>> GetCart(
            int customerId)
        {
            // http://localhost:33816/api/ShoppingCart/1
            return await GetCart($"{ServiceAddress}api/ShoppingCart/{customerId}");
        }
        internal static async Task<IList<CartRecordWithProductInfo>> GetCart(
            string cartUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(cartUrl);
                    if (!response.IsSuccessStatusCode) return null;
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<CartRecordWithProductInfo>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal static async Task<string> AddToCart(
            int customerId, int productId, int quantity)
        {
            //http://localhost:33816/api/shoppingcart/{customerId} HTTPPost
            //Note: ProductId and Quantity in the body
            //http://localhost:33816/api/shoppingcart/1 {"ProductId":22,"Quantity":2}
            //		Content-Type:application/json
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(
                        HttpMethod.Post,
                        $"{ServiceAddress}api/shoppingcart/{customerId}")
                    {
                        Content = new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}", Encoding.UTF8, "application/json")
                    };
                    //var response = await client.SendAsync(request);
                    var response = await client.PostAsync($"{ServiceAddress}api/shoppingcart/{customerId}",
                        new StringContent("{" + $"\"ProductId\":{productId},\"Quantity\":{quantity}" + "}",
                            Encoding.UTF8, "application/json"));
                    return !response.IsSuccessStatusCode ? null : response.Headers.Location.AbsoluteUri;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal static async Task<string> UpdateCartItem(
            int customerId, int id,  ShoppingCartRecord item)
        {
            // Change Cart Item(Quantity): http://localhost:33816/api/shoppingcart/{customerId}/{id} HTTPPut
            //   Note: Id, CustomerId, ProductId, TimeStamp, DateCreated, and Quantity in the body
            //{"Id":1,"CustomerId":1,"ProductId":33,"Quantity":2, "TimeStamp":"AAAAAAAA86s=","DateCreated":"1/20/2016"}
            //http://localhost:33816/api/shoppingcart/1/ts
            var json = JsonConvert.SerializeObject(item);
            try
            {
                using (var client = new HttpClient())
                {
                    var requestMessage = new HttpRequestMessage(
                        HttpMethod.Put,
                        $"{ServiceAddress}api/shoppingcart/{customerId}/{item.Id}");
                    requestMessage.Content = new StringContent(json,Encoding.UTF8,"application/json");
                    //var response = await client.SendAsync(requestMessage);
                    var response = await client.PutAsync($"{ServiceAddress}api/shoppingcart/{customerId}/{item.Id}", new StringContent(json, Encoding.UTF8, "application/json"));

                    return !response.IsSuccessStatusCode ? null : response.Headers.Location.AbsoluteUri;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        
        internal static async Task RemoveCartItem(
            int customerId, int id,byte[] timeStamp)
        {
            //Remove Cart Item: http://localhost:33816/api/shoppingcart/{customerId}/{id}/{TimeStamp} HTTPDelete
            //    http://localhost:33816/api/shoppingcart/1/2/AAAAAAAA1Uc=
            try
            {
                using (var client = new HttpClient())
                {
                    var timeStampString = JsonConvert.SerializeObject(timeStamp);
                    var response = await client.DeleteAsync($"{ServiceAddress}api/shoppingcart/{customerId}/{id}/{timeStampString}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal static async Task<int> PurchaseCart(int customerId, Customer customer)
        {
            //Purchase: http://localhost:33816/api/shoppingcart/{customerId}/buy HTTPPost
            //Note: Customer in the body
            //{ "Id":1,"FullName":"Super Spy","EmailAddress":"spy@secrets.com"}
            //  http://localhost:33816/api/shoppingcart/1/buy 
            var json = JsonConvert.SerializeObject(customer);
            try
            {
                //response.Headers.Location.AbsoluteUri
                using (var client = new HttpClient())
                {
                    var uri = $"{ServiceAddress}api/shoppingcart/{customerId}/buy";
                    var response = await client.PostAsync(uri,
                        new StringContent(json, Encoding.UTF8, "application/json"));
                                        
                    return !response.IsSuccessStatusCode ? 0 : int.Parse(await response.Content.ReadAsStringAsync());
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
