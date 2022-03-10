using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models;

namespace Scroll.Data;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int pageIndex,
        int pageSize)
    {
        var totalCount =
            await source.CountAsync();

        var items =
            await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

        var pagedList =
            new PagedList<T>(
                items,
                pageSize,
                pageIndex,
                totalCount);

        return pagedList;
    }

    public static async Task<PagedList<T>> ToPagedList<T>(
        this IAsyncEnumerable<T> source,
        int pageIndex,
        int pageSize)
    {
        var totalCount =
            await source.CountAsync();

        var items =
            await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

        var pagedList =
            new PagedList<T>(
                items,
                pageSize,
                pageIndex,
                totalCount);

        return pagedList;
    }
}