namespace Scroll.Domain.Entities;

public interface IEntity<TKey> where TKey : IEquatable<TKey>, IComparable<TKey>
{
    TKey Id { get; set; }
}