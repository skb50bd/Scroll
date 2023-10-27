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
    Task Delete(Guid id, CancellationToken token);
    Task<bool> Exists(Guid id, CancellationToken token);
    Task<ProductDto?> Get(Guid id, CancellationToken token);
    Task<ProductDto?> GetByTitle(string title, CancellationToken token);
    Task<ProductEditModel?> GetForEdit(Guid id, CancellationToken token);

    Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        Guid? categoryId = null,
        CancellationToken token = default
    );

    Task<Result<int?>> IncrementClickedCount(Guid productId, CancellationToken token);
    Task<Result<ProductDto>> Insert(ProductEditModel editModel, CancellationToken token);
    Task<Result<int>> NewProductFavorite(Guid userId, Guid productId, CancellationToken token);
    Task<Result<int>> UndoProductFavorite(Guid userId, Guid productId, CancellationToken token);
    Task<Result<ProductDto>> Update(ProductEditModel editModel, CancellationToken token);
    Task<bool> ProductIsLikedByUser(Guid productId, Guid userId, CancellationToken token);
}
