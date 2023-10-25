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
    Task Delete(Guid id);
    Task<bool> Exists(Guid id);
    Task<ProductDto?> Get(Guid id);
    Task<ProductDto?> GetByTitle(string title);
    Task<ProductEditModel?> GetForEdit(Guid id);

    Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        Guid? categoryId = null);

    Task<Result<int?>> IncrementClickedCount(Guid productId);
    Task<Result<ProductDto>> Insert(ProductEditModel editModel);
    Task<Result<int>> NewProductFavorite(Guid productId, string userName);
    Task<Result<ProductDto>> Update(ProductEditModel editModel);
}