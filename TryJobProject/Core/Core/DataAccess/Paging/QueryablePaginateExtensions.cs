using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Paging
{
    public static class QueryablePaginateExtensions
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(this IQueryable<T> source,int index , int size,CancellationToken cancellationToken)
        {
            int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            List<T> items = await source.Skip(index*size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

            Paginate<T> paginate = new()
            {
                Index = index,
                Size = size,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count /(double)size)
            };
            return paginate;
        }

        public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size)
        {
            var count = source.Count();
            var items = source.Skip(index * size).Take(size).ToList();

            Paginate<T> paginate = new()
            {
                Index = index,
                Size = size,
                Items = items,
                Count = count,
                Pages = (int)Math.Ceiling(count / (double)size)
             };
            return paginate;
        
        }
    }
}
