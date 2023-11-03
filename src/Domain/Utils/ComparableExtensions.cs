using Scroll.Common;

namespace Scroll.Domain.Utils;

public static class ComparableExtensions
{
    public static ComparableList<TItem> ToComparableList<TItem>(
        this IEnumerable<TItem> list
    )
    {
        var comparableList =
            new ComparableList<TItem>();

        comparableList.AddRange(list);

        return comparableList;
    }

    public static ComparableDictionary<TKey, TValue> ToComparableDict<TKey, TValue>(
        this IDictionary<TKey, TValue> dict
    ) where TKey : notnull
    {
        var comparableDict =
            new ComparableDictionary<TKey, TValue>();

        foreach (var kv in dict)
        {
            comparableDict[kv.Key] = kv.Value;
        }

        return comparableDict;
    }
}