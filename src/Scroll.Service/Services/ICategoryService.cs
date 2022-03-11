using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;

namespace Scroll.Service.Services;

public interface ICategoryService
{
    Task<bool> Delete(int id);
    Task<bool> Exists(int id);
    Task<CategoryDto?> Get(int id);
    Task<CategoryDto?> GetByName(string name);
    Task<CategoryEditModel?> GetForEdit(int id);
    Task<PagedList<CategoryDto>> GetPaged(int pageIndex = 0, int pageSize = 40, string? filterString = null);
    Task<int> GetProductCountInCategory(int categoryId);
    Task<PagedList<ProductDto>> GetProductsInCategory(int categoryId, int pageIndex = 0, int pageSize = 40);
    Task<CategoryDto> Insert(CategoryEditModel editModel);
    Task<CategoryDto> Update(CategoryEditModel editModel);
}