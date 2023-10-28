using LanguageExt.Common;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Services;

public enum ProductSortOrder
{
    IdAsc          = 1,
    IdDesc         = 2,
    NameAsc        = 3,
    NameDesc       = 4,
    FavoriteAsc    = 5,
    FavoriteDesc   = 6,
    ClickedAsc     = 7,
    ClickedDesc    = 8
}

public interface IProductService
{
    Task Delete(ProductId id, CancellationToken token);
    Task<bool> Exists(ProductId id, CancellationToken token);
    Task<ProductDto?> Get(ProductId id, CancellationToken token);
    Task<ProductDto?> GetByTitle(string title, CancellationToken token);
    Task<ProductEditModel?> GetForEdit(ProductId id, CancellationToken token);

    Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        CategoryId? categoryId = null,
        CancellationToken token = default
    );

    Task<Result<int?>> IncrementClickedCount(ProductId productId, CancellationToken token);
    Task<Result<ProductDto>> Insert(ProductEditModel editModel, CancellationToken token);
    Task<Result<int>> NewProductFavorite(Guid userId, ProductId productId, CancellationToken token);
    Task<Result<int>> UndoProductFavorite(Guid userId, ProductId productId, CancellationToken token);
    Task<Result<ProductDto>> Update(ProductEditModel editModel, CancellationToken token);
    Task<bool> ProductIsLikedByUser(Guid userId, ProductId productId, CancellationToken token);
}
