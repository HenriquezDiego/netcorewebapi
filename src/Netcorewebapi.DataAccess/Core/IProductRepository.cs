using ContaWebApi.DataAccess.Core.IRepositories;
using Netcorewebapi.Common;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;

namespace NetcorewebApi.DataAccess.Persistence.Repositories
{    public interface IProductRepository : IRepository<Product>
    {
        PageResult<Product> GetProductspaged(ProductParameters parameters);

    }
}
