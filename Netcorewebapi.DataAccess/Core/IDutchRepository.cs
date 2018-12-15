using System.Collections.Generic;
using Netcorewebapi.DataAccess.Data.Entities;
using Netcorewebapi.DataAccess.Persistence;

namespace Netcorewebapi.DataAccess.Core
{
    public interface IDutchRepository
    {
        ApplicationDbContext Context { get; }

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrders(bool include);

        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        Order GetOrderById(int id);
        Product GetProductsById(int id);
        void AddEntityOrder(Order model);
        void AddEntity(Product product);
    }
}