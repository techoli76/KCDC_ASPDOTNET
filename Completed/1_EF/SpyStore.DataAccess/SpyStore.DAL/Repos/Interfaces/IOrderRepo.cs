using System.Collections.Generic;
using SpyStore.DAL.Repos.Base;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos.Interfaces
{
    public interface IOrderRepo :IRepo<Order>
    {
        IEnumerable<OrderWithTotal> GetAllWithTotal(int customerId);
        OrderWithTotal GetOneWithDetails(int customerId, int orderId);
    }
}
