using Scroll.Library.Models.Entities;

namespace Scroll.Data.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<bool> Delete(T item);
    Task<bool> Delete(IEnumerable<T> items);
    Task<T> Add(T item);
    Task<List<T>> Add(IEnumerable<T> items);
    Task<T> Update(T item);
    Task<List<T>> Update(IEnumerable<T> items);
}

public interface IEntityRepository<T> : IRepository<T> where T : Entity
{
    Task<T?> Get(Guid id);
    Task<T> Upsert(T item);
    Task<bool> Delete(Guid id);
    Task<bool> Exists(Guid id);
}
