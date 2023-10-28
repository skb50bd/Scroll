using Scroll.Domain;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IProductRepository : IRepository<ProductId, Product>
{
    Task RemoveCategoriesFromProduct(
        ProductId productId,
        ISet<CategoryId> categories,
        CancellationToken token
    );
}