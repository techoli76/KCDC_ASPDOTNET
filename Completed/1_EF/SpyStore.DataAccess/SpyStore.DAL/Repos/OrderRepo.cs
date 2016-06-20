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
    public class OrderRepo : RepoBase<Order>, IOrderRepo
    {
        private readonly IOrderDetailRepo _orderDetailRepo;
        public OrderRepo(DbContextOptions<SpyStoreContext> options, IOrderDetailRepo orderDetailRepo) : base(options)
        {
            _orderDetailRepo = orderDetailRepo;
            Table = Context.Orders;
        }
        public OrderRepo(IOrderDetailRepo orderDetailRepo)
        {
            _orderDetailRepo = orderDetailRepo;
            Table = Context.Orders;
        }

        public IEnumerable<OrderWithTotal> GetAllWithTotal(int customerId) =>
            Table.Include(x => x.OrderDetails)
                .Where(x => x.CustomerId == customerId)
                .Select(x => new OrderWithTotal
                {
                    Id = x.Id,
                    CustomerId = customerId, 
                    OrderDate = x.OrderDate,
                    OrderTotal = (decimal?)x.OrderDetails.Sum(s => s.LineItemTotal),
                    ShipDate = x.ShipDate
                }).OrderBy(x => x.ShipDate);

        public OrderWithTotal GetOneWithDetails(
            int customerId, int orderId) =>
            Table.Include(x => x.OrderDetails)
                .Where(x => x.CustomerId == customerId && x.Id == orderId)
                .Select(x => new OrderWithTotal
                {
                    Id = x.Id,
                    CustomerId = customerId,
                    OrderDate = x.OrderDate,
                    OrderTotal = (decimal?)x.OrderDetails.Sum(s => s.LineItemTotal),
                    ShipDate = x.ShipDate,
                    OrderDetails = _orderDetailRepo.GetOrderDetails(orderId).ToList()
                }).FirstOrDefault();

    }
}

