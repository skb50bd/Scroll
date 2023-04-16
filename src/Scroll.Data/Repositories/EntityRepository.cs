using Microsoft.EntityFrameworkCore;
using Scroll.Data;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ScrollDbContext _dbContext;

    public Repository(ScrollDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> GetAll() =>
        _dbContext.Set<T>();

    public async Task<bool> Delete(T item)
    {
        _dbContext.Set<T>().Remove(item);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(IEnumerable<T> items)
    {
        _dbContext.Set<T>().RemoveRange(items);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<T> Add(T item)
    {
        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<T>> Add(IEnumerable<T> items)
    {
        var itemList = items.ToList();
        await _dbContext.AddRangeAsync(itemList);
        await _dbContext.SaveChangesAsync();
        return itemList;
    }

    public async Task<T> Update(T item)
    {
        _dbContext.Update(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<T>> Update(IEnumerable<T> items)
    {
        var itemList = items.ToList();
        foreach (var item in itemList)
        {
            _dbContext.Update(item);
        }
        
        await _dbContext.SaveChangesAsync();
        return itemList;
    }
}

public class EntityRepository<T> : Repository<T>, IEntityRepository<T> where T : Entity
{
    public EntityRepository(ScrollDbContext dbContext)
        : base(dbContext) { }

    public async Task<T?> Get(int id) =>
        await _dbContext.Set<T>().FindAsync(id);

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