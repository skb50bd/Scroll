﻿namespace Scroll.Common;

public class PagedList
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Count { get; set; }
    public int TotalCount { get; set; }
    public int TotalPageCount => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int LastPageIndex => TotalPageCount - 1;
    public bool IsFirstPage => PageIndex is 0;
    public bool IsLastPage => PageIndex == LastPageIndex;
    public bool HasNextPage => PageIndex < LastPageIndex;
    public bool HasPreviousPage => PageIndex > 0;
    public int FirstItemIndexInThisPage => PageIndex * PageSize;
    public int LastItemIndexInThisPage => FirstItemIndexInThisPage + Count - 1;
}

public class PagedList<T> : PagedList
{
    public List<T> Items { get; set; }

    public PagedList()
    {
        Items = [];
    }

    public PagedList(
        List<T> items,
        int pageSize,
        int pageIndex,
        int totalCount
    )
    {
        Items      = items;
        PageSize   = pageSize;
        PageIndex  = pageIndex;
        TotalCount = totalCount;
        Count      = items.Count;
    }

    public static PagedList<T> FromEnumerable(
        IList<T> source,
        int pageIndex,
        int pageSize) =>
            new(
                source
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList(),
                pageSize,
                pageIndex,
                source.Count
            );
}