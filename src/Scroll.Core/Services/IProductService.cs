using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;

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
    Task<bool> Delete(Guid id);
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

    Task<int?> IncrementClickedCount(Guid productId);
    Task<ProductDto> Insert(ProductEditModel editModel);
    Task<int> NewProductFavorite(Guid productId, string userName);
    Task<ProductDto> Update(ProductEditModel editModel);
}