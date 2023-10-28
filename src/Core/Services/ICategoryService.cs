﻿using LanguageExt.Common;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Services;

public interface ICategoryService
{
    Task Delete(CategoryId id, CancellationToken token);
    Task<bool> Exists(CategoryId id, CancellationToken token);
    Task<CategoryDto?> Get(CategoryId id, CancellationToken token);
    Task<CategoryDto?> GetByName(string name, CancellationToken token);
    Task<CategoryEditModel?> GetForEdit(CategoryId id, CancellationToken token);
    Task<PagedList<CategoryDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? filterString = null,
        CancellationToken token = default
    );

    IQueryable<CategoryDto> GetQueryable();
    Task<int> GetProductCountInCategory(CategoryId categoryId, CancellationToken token);
    Task<PagedList<ProductDto>> GetProductsInCategory(
        CategoryId categoryId,
        int pageIndex = 0,
        int pageSize = 40,
        CancellationToken token = default
    );

    Task<Result<CategoryDto>> Insert(CategoryEditModel editModel, CancellationToken token);
    Task<Result<CategoryDto>> Update(CategoryEditModel editModel, CancellationToken token);
}