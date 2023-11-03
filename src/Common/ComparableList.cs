namespace Scroll.Common;

public class ComparableList<TItem> : List<TItem>
{
    public void Add(IEnumerable<TItem> items) => AddRange(items);

    public bool Equals(ComparableList<TItem>? list)
    {
        if (this is null && list is null)
        {
            return true;
        }

        if (this is null || list is null)
        {
            return false;
        }

        if (Count != list.Count)
        {
            return false;
        }

        return this.SequenceEqual(list);
    }

    public override bool Equals(object? obj) =>
        Equals(obj as ComparableList<TItem>);

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
                + (item is null
                    || Equals(item, default(TItem))
                        ? 0
                        : item.GetHashCode()));
    }

    public static bool operator ==(
        ComparableList<TItem> left,
        ComparableList<TItem> right)
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
        ComparableList<TItem> left,
        ComparableList<TItem> right) =>
            (left == right) is false;
}