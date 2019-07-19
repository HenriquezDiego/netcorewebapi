using System.Linq;
using ContaWebApi.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Netcorewebapi.DataAccess.Entities;
using Netcorewebapi.DataAccess.Persistence;
using NetcorewebApi.DataAccess.Core;

namespace NetcorewebApi.DataAccess.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public Order GetOrderItems(int id)
        {
            return DbContext.Order
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        public OrderItem GetItemByOrderId(int orderId,int itemId)
        {  
            
            return DbContext.Order
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(order=>order.Id==orderId)
                .Items.FirstOrDefault(i=> i.Id==itemId);
        }
    }
}
