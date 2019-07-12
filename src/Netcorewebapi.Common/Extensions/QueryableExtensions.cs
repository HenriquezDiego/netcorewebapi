using Netcorewebapi.Common;
namespace System.Linq
{
    public static class QueryableExtensions
    {
        public static PageResult<T> ToPagedResult<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PageResult<T>(items,count,pageNumber,pageSize);
        }

    }
}
