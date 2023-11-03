using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories.EFCore;

public class UserRepository(
        AppDbContext dbCtx,
        ILogger<UserRepository> logger
    ) : Repository<Guid, User>(dbCtx, logger), IUserRepository
{
    public async Task<Option<User>> GetByEmail(string email, CancellationToken cancellationToken) =>
        await Table.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<Option<User>> GetByUserName(string userName, CancellationToken cancellationToken) =>
        await Table.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
}

public class ProductRepository(AppDbContext dbCtx, ILogger<ProductRepository> logger)
    : Repository<ProductId, Product>(dbCtx, logger), IProductRepository
{
    public async Task RemoveCategoriesFromProduct(
        ProductId productId,
        ISet<CategoryId> categories,
        CancellationToken token
    )
    {
        var product =
            await Table
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == productId, token);

        if (product is null)
        {
            logger.LogError(
                "Product with id {Id} not found to remove categories",
                productId
            );
            return;
        }

        foreach (var map in product.ProductCategories.Where(pc => categories.Contains(pc.CategoryId)))
        {
            product.ProductCategories.Remove(map);
        }

        await SaveChanges(token);
    }
}