using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
    public class OrderDetailRepo : RepoBase<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(DbContextOptions<SpyStoreContext> options) : base(options)
        {
            Table = Context.OrderDetails;
        }
        public OrderDetailRepo()
        {
            Table = Context.OrderDetails;
        }

        public IEnumerable<OrderDetailWithProductInfo> GetAllWithDetails(int customerId) => Table
            .Include(x => x.Product)
            .ThenInclude(p => p.Category)
                .Where(x => x.Order.CustomerId == customerId)
                .Select(x => new OrderDetailWithProductInfo
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitCost = x.UnitCost,
                    LineItemTotal = x.LineItemTotal,
                    Description = x.Product.Description,
                    ModelName = x.Product.ModelName,
                    ProductImage = x.Product.ProductImage,
                    ProductImageLarge = x.Product.ProductImageLarge,
                    ProductImageThumb = x.Product.ProductImageThumb,
                    ModelNumber = x.Product.ModelNumber,
                    CategoryName = x.Product.Category.CategoryName
                }).OrderBy(x => x.ModelName);

        public IEnumerable<OrderDetailWithProductInfo> GetOrderDetails(int orderId) => Table
            .Include(x => x.Product)
            .ThenInclude(p => p.Category)
                .Where(x => x.Order.Id == orderId)
                .Select(x => new OrderDetailWithProductInfo
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitCost = x.UnitCost,
                    LineItemTotal = x.LineItemTotal,
                    Description = x.Product.Description,
                    ModelName = x.Product.ModelName,
                    ProductImage = x.Product.ProductImage,
                    ProductImageLarge = x.Product.ProductImageLarge,
                    ProductImageThumb = x.Product.ProductImageThumb,
                    ModelNumber = x.Product.ModelNumber,
                    CategoryName = x.Product.Category.CategoryName
                }).OrderBy(x => x.ModelName);

    }
}

