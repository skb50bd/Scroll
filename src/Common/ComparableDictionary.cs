namespace Scroll.Common;

public class ComparableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
{
    public void Add(IEnumerable<KeyValuePair<TKey, TValue>> kvs)
    {
        foreach (var kv in kvs)
        {
            if (ContainsKey(kv.Key) is false)
            {
                this[kv.Key] = kv.Value;
            }
            else
            {
                throw new ArgumentException(
                    $"An item with the same key [{kv.Key}] has already been added");
            }
        }
    }

    public bool Equals(ComparableDictionary<TKey, TValue>? dict)
    {
        if (this is null && dict is null)
        {
            return true;
        }

        if (this is null || dict is null)
        {
            return false;
        }

        if (Count != dict.Count)
        {
            return false;
        }

        return
            Keys.SequenceEqual(dict.Keys)
            && Values.SequenceEqual(dict.Values);
    }

    public override bool Equals(object? obj) =>
        Equals(obj as ComparableDictionary<TKey, TValue>);

    public override int GetHashCode()
    {
        if (this is null)
        {
            return 0;
        }

        return this.Aggregate(
            0x2D2816FE,
            (current, item) =>
                (current * 397)
                + (Equals(item.Key, default(TKey))
                    ? 0
                    : item.Key!.GetHashCode())
                + (Equals(item.Value, default(TValue))
                    ? 0
                    : item.Value!.GetHashCode()));
    }

    public static bool operator ==(
        ComparableDictionary<TKey, TValue> left,
        ComparableDictionary<TKey, TValue> right
    )
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(
        ComparableDictionary<TKey, TValue> left,
        ComparableDictionary<TKey, TValue> right) =>
            (left == right) is false;
}
