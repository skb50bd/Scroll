using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;

namespace Scroll.Core.Services;

public interface ICategoryService
{
    Task<bool> Delete(Guid id);
    Task<bool> Exists(Guid id);
    Task<CategoryDto?> Get(Guid id);
    Task<CategoryDto?> GetByName(string name);
    Task<CategoryEditModel?> GetForEdit(Guid id);
    Task<PagedList<CategoryDto>> GetPaged(int pageIndex = 0, int pageSize = 40, string? filterString = null);
    IQueryable<CategoryDto> GetQueryable();
    Task<int> GetProductCountInCategory(Guid categoryId);
    Task<PagedList<ProductDto>> GetProductsInCategory(Guid categoryId, int pageIndex = 0, int pageSize = 40);
    Task<CategoryDto> Insert(CategoryEditModel editModel);
    Task<CategoryDto> Update(CategoryEditModel editModel);
}