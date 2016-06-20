using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;
using SpyStore.MVC.Controllers.Base;
using SpyStore.MVC.DataAccess;
using SpyStore.MVC.Models.ViewModels;

namespace SpyStore.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : SpyStoreBaseController
    {
        MapperConfiguration _config = null;
        public CartController()
        {
            _config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CartRecordViewModel, ShoppingCartRecord>();
                    cfg.CreateMap<CartRecordWithProductInfo, CartRecordViewModel>();
                    cfg.CreateMap<ProductWithCategoryName, AddToCartViewModel>();
                    //cfg.CreateMap<Product, Product>()
                    //.ForMember(x => x.Category, opt => opt.Ignore())
                    //.ForMember(x => x.OrderDetails, opt => opt.Ignore())
                    //.ForMember(x => x.Reviews, opt => opt.Ignore())
                    //.ForMember(x => x.ShoppingCartRecords, opt => opt.Ignore());
                });
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> Index(int customerId, string returnUrl)
        {
            ViewBag.Title = "Cart";
            ViewBag.Header = "Cart";
            //ViewBag.ReturnUrl = Request.Path + Request.QueryString;
            ViewBag.ReturnUrl = returnUrl;
            var cartItems = await WebAPICalls.GetCart(customerId);
            var customer = await WebAPICalls.GetCustomer(customerId);
            var mapper = _config.CreateMapper();
            var viewModel = new CartViewModel
            {
                Customer = customer,
                CartRecords = mapper.Map<IList<CartRecordViewModel>>(cartItems)
            };
            return View(viewModel);
        }

        [HttpGet("{customerId}/{Id?}")]
        public async Task<IActionResult> AddToCart(
            int Id, int customerId, string returnUrl, int quantity = 1)
        {
            ViewBag.Title = "Add to Cart";
            ViewBag.Header = "Add to Cart";
            ViewBag.ReturnUrl = returnUrl;
            var prod = await WebAPICalls.GetOneProduct(Id);
            var mapper = _config.CreateMapper();
            var cartRecord = mapper.Map<AddToCartViewModel>(prod);
            cartRecord.CustomerId = customerId;
            cartRecord.Quantity = quantity;
            return View(cartRecord);
        }

        [HttpPost("{customerId}/{Id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(
            int Id,
            int customerId,
            int quantity,
            string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var cartUrl = await WebAPICalls.AddToCart(customerId,Id,quantity);
            return RedirectToAction(nameof(CartController.Index), new {customerId,returnUrl});
        }

        [HttpPost("{customerId}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(
            int customerId, int id, [Bind(Prefix = "item")] CartRecordViewModel item)
        {
            if (!ModelState.IsValid) return View(item);

            if (id != item.Id)
            {
                return new BadRequestResult();
            }
            var mapper = _config.CreateMapper();
            var newItem = mapper.Map<ShoppingCartRecord>(item);
            await WebAPICalls.UpdateCartItem(customerId, id, newItem);
            return RedirectToAction(nameof(CartController.Index),
                new { customerId });
        }

        [HttpPost("{customerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int customerId, Customer customer)
        {
            if (customerId != customer.Id)
            {
                return new BadRequestResult();
            }
            if (!ModelState.IsValid)
            {
                //TODO: Handle this better
                throw new Exception("Oops");
                //return RedirectToAction();
            }
            int orderId = await WebAPICalls.PurchaseCart(customerId, customer);
            return RedirectToAction(nameof(OrderController.Details),"Order",new { customerId,orderId });
        }

        [HttpPost("{customerId}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(
            int customerId, int id, ShoppingCartRecord item)
        {
            if (id != item.Id)
            {
                return new BadRequestResult();
            }
            await WebAPICalls.RemoveCartItem(customerId, id, item.TimeStamp);
            return RedirectToAction(nameof(CartController.Index),
                new {customerId});
        }
    }
}
