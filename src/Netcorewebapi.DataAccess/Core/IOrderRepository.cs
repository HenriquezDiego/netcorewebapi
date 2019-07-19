using ContaWebApi.DataAccess.Core.IRepositories;
using Netcorewebapi.DataAccess.Entities;

namespace NetcorewebApi.DataAccess.Core
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderItems(int id);
        OrderItem GetItemByOrderId(int orderId, int itemId);
    }
    
}
