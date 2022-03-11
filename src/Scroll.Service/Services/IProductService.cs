using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;

namespace Scroll.Service.Services;

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
    Task<bool> Delete(int id);
    Task<bool> Exists(int id);
    Task<ProductDto?> Get(int id);
    Task<ProductDto?> GetByTitle(string title);
    Task<ProductEditModel?> GetForEdit(int id);

    Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        int? categoryId = null);

    Task<int?> IncrementClickedCount(int productId);
    Task<ProductDto> Insert(ProductEditModel editModel);
    Task<int> NewProductFavorite(int productId, string userName);
    Task<ProductDto> Update(ProductEditModel editModel);
}
