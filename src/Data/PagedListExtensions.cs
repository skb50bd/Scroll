using Microsoft.EntityFrameworkCore;
using Scroll.Domain;

namespace Scroll.Data;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var totalCount =
            await source.CountAsync(cancellationToken);

        var items =
            await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        var pagedList =
            new PagedList<T>(
                items,
                pageSize,
                pageIndex,
                totalCount
            );

        return pagedList;
    }
}