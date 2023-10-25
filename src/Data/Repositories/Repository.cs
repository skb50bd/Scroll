using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public class Repository<T>(
        AppDbContext dbCtx,
        ILogger<Repository<T>> logger
    ) : IRepository<T> where T: class, IBaseEntity
{
    private ILogger<Repository<T>> _logger = logger;
    protected AppDbContext DbCtx = dbCtx;
    protected DbSet<T> Set => DbCtx.Set<T>();
    public IQueryable<T> Table => Set;

    public virtual ValueTask<T?> GetById(Guid id) =>
        Set.FindAsync(id);

    public virtual Task<List<T>> GetAll() =>
        Table.ToListAsync();

    public async Task Add(T item)
    {
        await Set.AddAsync(item);
        await SaveChanges();
    }

    public async ValueTask AddNoSave(T item)
    {
        await Set.AddAsync(item);
    }

    public async Task Add(IEnumerable<T> items)
    {
        await AddNoSave(items);
        await SaveChanges();
    }

    public Task AddNoSave(IEnumerable<T> items) => Set.AddRangeAsync(items);
    public Task Update(T item) => SaveChanges();
    public void UpdateNoSave(T item) {}
    public Task Update(IEnumerable<T> items) => SaveChanges();
    public void UpdateNoSave(IEnumerable<T> items) {}

    public async Task Delete(Guid id)
    {
        var item = await Set.FindAsync(id);
        if (item is null)
        {
            _logger.LogError(
                "Item with id {Id} not found to delete",
                id
            );
            return;
        }

        Set.Remove(item);
        await SaveChanges();
    }

    public Task Delete(T item)
    {
        Set.Remove(item);
        return SaveChanges();
    }

    public void DeleteNoSave(T item) => Set.Remove(item);

    public Task Delete(IEnumerable<T> items)
    {
        Set.RemoveRange(items);
        return SaveChanges();
    }

    public void DeleteNoSave(IEnumerable<T> items) =>  Set.RemoveRange(items);
    public Task<bool> Exists(Guid id) => Set.AnyAsync(i => i.Id == id);
    public Task SaveChanges() => DbCtx.SaveChangesAsync();
}