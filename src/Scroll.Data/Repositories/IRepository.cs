using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public interface IRepository<T> where T : Entity
{
    Task<T?> Get(int id);
    IQueryable<T> GetAll();
    Task<T> Upsert(T item);
    Task<bool> Delete(int id);
}