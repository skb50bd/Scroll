using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ScrollDbContext DbContext;

    public Repository(ScrollDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IQueryable<T> GetAll() =>
        DbContext.Set<T>();

    public async Task<bool> Delete(T item)
    {
        DbContext.Set<T>().Remove(item);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(IEnumerable<T> items)
    {
        DbContext.Set<T>().RemoveRange(items);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<T> Add(T item)
    {
        await DbContext.AddAsync(item);
        await DbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<T>> Add(IEnumerable<T> items)
    {
        var itemList = items.ToList();
        await DbContext.AddRangeAsync(itemList);
        await DbContext.SaveChangesAsync();
        return itemList;
    }

    public async Task<T> Update(T item)
    {
        DbContext.Update(item);
        await DbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<T>> Update(IEnumerable<T> items)
    {
        var itemList = items.ToList();
        foreach (var item in itemList)
        {
            DbContext.Update(item);
        }

        await DbContext.SaveChangesAsync();
        return itemList;
    }
}

public class EntityRepository<T> : Repository<T>, IEntityRepository<T> where T : Entity
{
    public EntityRepository(ScrollDbContext dbContext)
        : base(dbContext) { }

    public async Task<T?> Get(Guid id) =>
        await DbContext.Set<T>().FindAsync(id);

    public async Task<T> Upsert(T item)
    {
        if (item.Id == default)
        {
            await DbContext.AddAsync(item);
        }
        else
        {
            DbContext.Update(item);
        }

        await DbContext.SaveChangesAsync();

        return item;
    }

    public async Task<bool> Delete(Guid id)
    {
        var item = await Get(id);

        if (item is null)
        {
            return false;
        }

        DbContext.Remove(item);

        var numOfRowsAffected =
            await DbContext.SaveChangesAsync();

        return numOfRowsAffected > 0;
    }

    public Task<bool> Exists(Guid id) =>
        DbContext
            .Set<T>()
            .AnyAsync(p => p.Id == id);
}