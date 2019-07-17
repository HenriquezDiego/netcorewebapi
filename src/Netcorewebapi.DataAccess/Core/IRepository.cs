using Netcorewebapi.Common;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;
using Netcorewebapi.DataAccess.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netcorewebapi.DataAccess.Core
{
    public interface IRepository
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
        Task<bool> AddEntityAsync(Product product);
        PageResult<Product> GetProductsPage(ProductParameters resourceParameters);
    }
}