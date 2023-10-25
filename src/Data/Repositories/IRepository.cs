using Microsoft.EntityFrameworkCore.ChangeTracking;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IRepository<T> where T: IBaseEntity
{
    IQueryable<T> Table { get; }
    ValueTask<T?> GetById(Guid id);
    Task<List<T>> GetAll();
    Task Add(T item);
    ValueTask AddNoSave(T item);
    Task Add(IEnumerable<T> items);
    Task AddNoSave(IEnumerable<T> items);
    Task Update(T item);
    void UpdateNoSave(T item);
    Task Update(IEnumerable<T> items);
    void UpdateNoSave(IEnumerable<T> items);
    Task Delete(Guid id);
    Task Delete(T item);
    void DeleteNoSave(T item);
    Task Delete(IEnumerable<T> items);
    void DeleteNoSave(IEnumerable<T> items);
    Task<bool> Exists(Guid id);
    Task SaveChanges();
}