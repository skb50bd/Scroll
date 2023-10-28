using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories.EFCore;

public class Repository<TKey, T>(
        AppDbContext dbCtx,
        ILogger<Repository<TKey, T>> logger
    ) : IRepository<TKey, T> where TKey : IEquatable<TKey>, IComparable<TKey> where T : class, IEntity<TKey>
{
    protected DbSet<T> Set => dbCtx.Set<T>();
    public IQueryable<T> Table => Set;

    public virtual ValueTask<T?> GetById(TKey id, CancellationToken token) =>
        Set.FindAsync(id, token);

    public virtual Task<List<T>> GetAll(CancellationToken token) =>
        Table.ToListAsync(token);

    public async Task Add(T item, CancellationToken token)
    {
        await Set.AddAsync(item, token);
        await SaveChanges(token);
    }

    public async ValueTask AddNoSave(T item, CancellationToken token)
    {
        await Set.AddAsync(item, token);
    }

    public async Task Add(IEnumerable<T> items, CancellationToken token)
    {
        await AddNoSave(items, token);
        await SaveChanges(token);
    }

    public Task AddNoSave(IEnumerable<T> items, CancellationToken token) => Set.AddRangeAsync(items, token);
    public Task Update(T item, CancellationToken token) => SaveChanges(token);
    public void UpdateNoSave(T item) {}
    public Task Update(IEnumerable<T> items, CancellationToken token) => SaveChanges(token);
    public void UpdateNoSave(IEnumerable<T> items) {}

    public async Task Delete(TKey id, CancellationToken token)
    {
        var item = await Set.FindAsync(id, token);
        if (item is null)
        {
            logger.LogError(
                "Item with id {Id} not found to delete",
                id
            );
            return;
        }

        Set.Remove(item);
        await SaveChanges(token);
    }

    public Task Delete(T item, CancellationToken token)
    {
        Set.Remove(item);
        return SaveChanges(token);
    }

    public void DeleteNoSave(T item) => Set.Remove(item);

    public Task Delete(IEnumerable<T> items, CancellationToken token)
    {
        Set.RemoveRange(items);
        return SaveChanges(token);
    }

    public void DeleteNoSave(IEnumerable<T> items) =>  Set.RemoveRange(items);

    public async Task<bool> Exists(TKey id, CancellationToken token)
    {
        // TODO: Fix this to use AnyAsync
        var item = await Set.FindAsync(id, token);
        return item is not null;
    }

    public Task SaveChanges(CancellationToken token) => dbCtx.SaveChangesAsync(token);
}