using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IRepository<TKey, T> where TKey : IEquatable<TKey>, IComparable<TKey> where T : class, IEntity<TKey>
{
    IQueryable<T> Table { get; }
    ValueTask<T?> GetById(TKey id, CancellationToken token);
    Task<List<T>> GetAll(CancellationToken token);
    Task Add(T item, CancellationToken token);
    ValueTask AddNoSave(T item, CancellationToken token);
    Task Add(IEnumerable<T> items, CancellationToken token);
    Task AddNoSave(IEnumerable<T> items, CancellationToken token);
    Task Update(T item, CancellationToken token);
    void UpdateNoSave(T item);
    Task Update(IEnumerable<T> items, CancellationToken token);
    void UpdateNoSave(IEnumerable<T> items);
    Task Delete(TKey id, CancellationToken token);
    Task Delete(T item, CancellationToken token);
    void DeleteNoSave(T item);
    Task Delete(IEnumerable<T> items, CancellationToken token);
    void DeleteNoSave(IEnumerable<T> items);
    Task<bool> Exists(TKey id, CancellationToken token);
    Task SaveChanges(CancellationToken token);
}