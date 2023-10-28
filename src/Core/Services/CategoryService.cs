using FluentValidation;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Scroll.Core.Extensions;
using Scroll.Core.ObjectMapping;
using Scroll.Data;
using Scroll.Data.Repositories;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.Exceptions;
using Scroll.Domain.InputModels;
using Scroll.Domain.Utils;
using ValidationException = FluentValidation.ValidationException;

namespace Scroll.Core.Services;

public class CategoryService(
    IRepository<CategoryId, Category> repo,
    IValidator<CategoryEditModel> validator
    ) : ICategoryService
{
    public Task<CategoryDto?> Get(CategoryId id, CancellationToken token) =>
        repo.GetById(id, token).ToDtoAsync();

    public Task<CategoryDto?> GetByName(string name, CancellationToken token) =>
        repo.Table
            .Where(c => c.Name == name)
            .FirstOrDefaultAsync(token)
            .ToDtoAsync();

    public Task<CategoryEditModel?> GetForEdit(CategoryId id, CancellationToken token) =>
        repo.GetById(id, token).ToEditModelAsync();

    public IQueryable<CategoryDto> GetQueryable() =>
        repo.Table.ProjectToDto();

    public async Task<PagedList<CategoryDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? filterString = null,
        CancellationToken token = default
    )
    {
        var query = repo.Table;

        if (filterString.IsNotBlank())
        {
            query =
                query.Where(c =>
                    c.Name.Contains(filterString));
        }

        var categories =
            await query
                .OrderBy(c => c.Name)
                .ToPagedList(pageIndex, pageSize, token);

        return categories.ProjectToDto();
    }

    public async Task<Result<CategoryDto>> Insert(
        CategoryEditModel editModel,
        CancellationToken token
    )
    {
        var validationResult =
            await validator.ValidateAsync(editModel, token);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var category = editModel.ToEntity() ?? throw StandardErrors.Unreachable;
        await repo.Add(category, token);
        return category.ToDto().RequireNotNull();
    }

    public async Task<Result<CategoryDto>> Update(
        CategoryEditModel editModel,
        CancellationToken token
    )
    {
        var validationResult =
            await validator.ValidateAsync(editModel, token);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var entity =
            await repo.GetById(new(editModel.Id), token)
            ?? throw new ArgumentException("Invalid Update Operation. Entity doesn't exist");

        editModel.ToEntity(entity);
        entity.RequireNotNull();

        await repo.Update(entity, token);
        return entity.ToDto().RequireNotNull();
    }

    public Task Delete(CategoryId id, CancellationToken token) => repo.Delete(id, token);
    public Task<bool> Exists(CategoryId id, CancellationToken token) => repo.Exists(id, token);

    public async Task<PagedList<ProductDto>> GetProductsInCategory(
        CategoryId categoryId,
        int pageIndex = 0,
        int pageSize = 40,
        CancellationToken token = default
    )
    {
        var productsInCategory =
            await repo.Table
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Products)
                .ToPagedList(pageIndex, pageSize, token);

        return productsInCategory.ProjectToDto();
    }

    public async Task<int> GetProductCountInCategory(CategoryId categoryId, CancellationToken token)
    {
        var productCount =
            await repo.Table
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Products)
                .CountAsync(token);

        return productCount;
    }
}