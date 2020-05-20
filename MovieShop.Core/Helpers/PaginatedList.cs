using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.Helpers
{
    public class PaginatedList<T>: List<T>
    {
        public PaginatedList(List<T> items, int count, int page, int pageSize)
        {
            PageIndex = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            AddRange(items);
        }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public static async Task<PaginatedList<T>> GetPaged(IQueryable<T> source, int pageIndex, int pageSize,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderedQuery = null,
                                                Expression<Func<T, bool>> filter = null)
        {
            var query = source;
            if (filter != null) query = query.Where(filter);
            if (orderedQuery != null) query = orderedQuery(query);
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
