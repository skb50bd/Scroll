using Microsoft.EntityFrameworkCore;
using Scroll.Data;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly ScrollDbContext _dbContext;

    public Repository(ScrollDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> Get(int id) =>
        await _dbContext.Set<T>().FindAsync(id);

    public IQueryable<T> GetAll() =>
        _dbContext.Set<T>();

    public async Task<T> Upsert(T item)
    {
        if (item.Id is 0)
        {
            await _dbContext.AddAsync(item);
        }
        else
        {
            _dbContext.Update(item);
        }

        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await Get(id);

        if (item is null)
        {
            return false;
        }

        _dbContext.Remove(item);

        var numOfRowsAffected =
            await _dbContext.SaveChangesAsync();

        return numOfRowsAffected > 0;
    }

    public Task<bool> Exists(int id) =>
        _dbContext
            .Set<T>()
            .AnyAsync(p => p.Id == id);
}