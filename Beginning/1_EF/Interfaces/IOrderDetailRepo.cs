using System.Collections.Generic;
using SpyStore.DAL.Repos.Base;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos.Interfaces
{
    public interface IOrderDetailRepo :IRepo<OrderDetail>
    {
        IEnumerable<OrderDetailWithProductInfo> GetAllWithDetails(int customerId);
        IEnumerable<OrderDetailWithProductInfo> GetOrderDetails(int orderId);
    }
}
