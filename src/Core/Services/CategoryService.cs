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

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repo;
    private readonly IValidator<CategoryEditModel> _validator;
    public CategoryService(
        IRepository<Category> repo,
        IValidator<CategoryEditModel> validator
    )
    {
        _repo           = repo;
        _validator      = validator;
    }

    public Task<CategoryDto?> Get(Guid id) =>
        _repo.GetById(id).ToDtoAsync();

    public Task<CategoryDto?> GetByName(string name) =>
        _repo.Table
            .Where(c => c.Name == name)
            .FirstOrDefaultAsync()
            .ToDtoAsync();

    public Task<CategoryEditModel?> GetForEdit(Guid id) =>
        _repo.GetById(id)
            .ToEditModelAsync();

    public IQueryable<CategoryDto> GetQueryable() =>
        _repo.Table
            .ProjectToDto();

    public async Task<PagedList<CategoryDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? filterString = null)
    {
        var query = _repo.Table;

        if (filterString.IsNotBlank())
        {
            query =
                query.Where(c =>
                    c.Name.Contains(filterString));
        }

        var categories =
            await query
                    .OrderBy(c => c.Name)
                    .ToPagedList(pageIndex, pageSize);

        return categories.ProjectToDto();
    }

    public async Task<Result<CategoryDto>> Insert(CategoryEditModel editModel)
    {
        var validationResult = 
            await _validator.ValidateAsync(editModel);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }
        
        var category = editModel.ToEntity();
        
        if (category is null)
        {
            throw StandardErrors.Unreachable;
        }
        
        await _repo.Add(category);
        return category.ToDto().RequireNotNull();
    }

    public async Task<Result<CategoryDto>> Update(
        CategoryEditModel editModel)
    {
        var validationResult = 
            await _validator.ValidateAsync(editModel);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var entity =
            await _repo.GetById(editModel.Id);

        if (entity is null)
        {
            throw new ArgumentException(
                "Invalid Update Operation. Entity doesn't exist");
        }

        editModel.ToEntity(entity);
        entity.RequireNotNull();
        
        await _repo.Update(entity);
        return entity.ToDto().RequireNotNull();
    }

    public Task Delete(Guid id) => _repo.Delete(id);
    public Task<bool> Exists(Guid id) => _repo.Exists(id);

    public async Task<PagedList<ProductDto>> GetProductsInCategory(
        Guid categoryId,
        int pageIndex = 0,
        int pageSize = 40)
    {
        var productsInCategory =
            await _repo.Table
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Products)
                .ToPagedList(pageIndex, pageSize);

        return productsInCategory.ProjectToDto();
    }

    public async Task<int> GetProductCountInCategory(Guid categoryId)
    {
        var productCount =
            await _repo.Table
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Products)
                .CountAsync();

        return productCount;
    }
}