using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<bool> Delete(T item);
    Task<T> Add(T item);
    Task<T> Update(T item);
}

public interface IEntityRepository<T> : IRepository<T> where T : Entity
{
    Task<T?> Get(int id);
    Task<T> Upsert(T item);
    Task<bool> Delete(int id);
    Task<bool> Exists(int id);
}
