using ContaWebApi.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Netcorewebapi.Common;
using Netcorewebapi.Common.Helpers;
using Netcorewebapi.DataAccess.Entities;
using Netcorewebapi.DataAccess.Persistence;
using System.Linq;

namespace NetcorewebApi.DataAccess.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public PageResult<Product> GetProductspaged(ProductParameters parameters)
        {
            var collectionBeforePaging = DbContext.Products
                .OrderBy(p => p.ArtDescription)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Category))
            {
                // trim & ignore casing
                var catForWhereClause = parameters.Category
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Category.ToLowerInvariant() == catForWhereClause);
            }

            
            if (!string.IsNullOrEmpty(parameters.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = parameters.SearchQuery
                    .Trim().ToLowerInvariant();
            
                collectionBeforePaging = collectionBeforePaging.AsQueryable()
                    .Where(a => a.Title.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                ||a.Category.ToLowerInvariant().Contains(searchQueryForWhereClause));

            }

            return collectionBeforePaging
                .ToPagedResult(parameters.Page,parameters.PerPage);
        }
    }
}
