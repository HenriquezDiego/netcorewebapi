using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Netcorewebapi.DataAccess.Core;
using Netcorewebapi.DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netcorewebapi.DataAccess.Persistence
{
    public class Repository : IRepository
    {
        public ApplicationDbContext Context { get; }
        private ILogger<Repository> Logger { get; }

        public Repository(ApplicationDbContext context, ILogger<Repository> logger)
        {
            Context = context;
            Logger = logger;
        }


        public IEnumerable<Product> GetAllProducts() {
            Logger.LogInformation("GetAllProducts was called");
            return Context.Products
                    .OrderBy(p => p.Title)
                    .ToList();

        }

        public IEnumerable<Product> GetProductsByCategory(string category) {

            return Context.Products
                           .Where(c => c.Category == category)
                           .ToList();
        }

        

        public IEnumerable<Order> GetAllOrders()
        {
            return Context.Order
                .Include(o => o.Items)
                .ThenInclude(i=> i.Product)
                .ToList();
        }

        public Order GetOrderById(int id)
        {

            return Context.Order
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(o => o.Id == id);
        }

        public void AddEntityOrder(Order model)
        {
            Context.Add(model);

            Context.SaveChanges();
        }

        public void AddEntity(Product model)
        {
            Context.Add(model);
            Context.SaveChanges();
        }

        public Product GetProductsById(int id)
        {
            var product = Context.Products.FirstOrDefault(p => p.Id == id);
            
            return product;
        }

        public IEnumerable<Order> GetAllOrders(bool include)
        {
            if (include)
            {
                return Context.Order
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }

            return Context.Order.ToList();
        }

        public async Task<bool> AddEntityAsync(Product product)
        {
            Context.Products.Add(product);
            var flag = await Context.SaveChangesAsync();
            return flag > 0;
        }
    }
}
