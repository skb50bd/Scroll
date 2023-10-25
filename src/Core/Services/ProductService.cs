using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scroll.Core.Extensions;
using Scroll.Core.ObjectMapping;
using Scroll.Data;
using Scroll.Data.Repositories;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;
using Scroll.Domain.Utils;
using static Scroll.Core.Services.ProductSortOrder;

namespace Scroll.Core.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepo;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<ProductCategoryMapping> _productCategoryMappingRepo;
    private readonly IValidator<ProductEditModel> _productValidator;

    public ProductService(
        IRepository<Product> productRepo,
        UserManager<User> userManager,
        IRepository<ProductCategoryMapping> productCategoryMappingRepo, 
        IValidator<ProductEditModel> productValidator
    )
    {
        _productRepo                = productRepo;
        _productCategoryMappingRepo = productCategoryMappingRepo;
        _productValidator           = productValidator;
        _userManager                = userManager;
    }

    public Task<ProductDto?> Get(Guid id) =>
        _productRepo
            .Table
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id)
            .ToDtoAsync();

    public Task<ProductDto?> GetByTitle(string title) =>
        _productRepo
            .Table
            .Include(p => p.Categories)
            .Where(p => p.Title == title)
            .FirstOrDefaultAsync()
            .ToDtoAsync();

    public Task<ProductEditModel?> GetForEdit(Guid id) =>
        _productRepo
            .Table
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id)
            .ToEditModelAsync();

    public async Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = IdDesc,
        Guid? categoryId = null)
    {
        var query =
            _productRepo.Table;

        if (categoryId != default)
        {
            query =
                query.Where(product =>
                    product.Categories.Any(cat => cat.Id == categoryId)
                );
        }

        if (searchString.IsNotBlank())
        {
            query =
                query.Where(c =>
                    c.Title.Contains(searchString));
        }

        query =
            sortBy switch
            {
                IdAsc        => query.OrderBy(c => c.Id),
                IdDesc       => query.OrderByDescending(c => c.Id),
                NameAsc      => query.OrderBy(p => p.Title),
                NameDesc     => query.OrderByDescending(p => p.Title),
                FavoriteAsc  => query.OrderBy(p => p.FavoriteCount),
                FavoriteDesc => query.OrderByDescending(p => p.FavoriteCount),
                ClickedAsc   => query.OrderBy(p => p.ClickCount),
                ClickedDesc  => query.OrderByDescending(p => p.ClickCount),
                _            => throw new NotImplementedException(
                                    $"{sortBy} not implemented.")
            };

        var products =
            await query
                .Include(p => p.Categories)
                .ToPagedList(pageIndex, pageSize);

        return products.ProjectToDto();
    }

    public async Task<Result<ProductDto>> Insert(ProductEditModel editModel)
    {
        var validationResult = 
            await _productValidator.ValidateAsync(editModel);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var entity = editModel.ToEntity().RequireNotNull();
        editModel.CategoryIds ??= new();

        var productCategories =
            editModel.CategoryIds
                .Select(cId => new ProductCategoryMapping
                {
                    ProductId  = editModel.Id,
                    CategoryId = cId
                })
                .ToList();

        entity.ProductCategories = productCategories;

        await _productRepo.Add(entity);
        return entity.ToDto().RequireNotNull();
    }

    public async Task<Result<ProductDto>> Update(ProductEditModel editModel)
    {
        var validationResult = 
            await _productValidator.ValidateAsync(editModel);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }
        
        var originalProduct =
            await _productRepo.Table
                    .Include(p => p.ProductCategories)
                    .FirstOrDefaultAsync(p => p.Id == editModel.Id);

        if (originalProduct is null)
        {
            return new(new ArgumentException(
                    "Invalid Update Operation. Entity doesn't exist"
                )
            );
        }

        editModel.ToEntity(originalProduct);
        
        var originalCategoryIds =
            originalProduct.ProductCategories
                .Select(pc => pc.CategoryId)
                .ToHashSet();

        editModel.CategoryIds ??= new();

        var removedCategoryIds =
            originalCategoryIds
                .Except(editModel.CategoryIds)
                .ToHashSet();

        if (removedCategoryIds.Any())
        {
            var removedProductCategories =
                await _productCategoryMappingRepo.Table
                        .Where(pc =>
                            removedCategoryIds.Contains(pc.CategoryId)
                            && pc.ProductId == editModel.Id)
                        .ToListAsync();

            foreach (var pcMap in removedProductCategories)
            {
                await _productCategoryMappingRepo.Delete(pcMap);
            }
        }

        var newCategories =
            editModel.CategoryIds
                .Except(originalCategoryIds);

        if (newCategories.Any())
        {
            var productCategories =
                editModel.CategoryIds
                    .Select(cId => new ProductCategoryMapping
                    {
                        ProductId  = editModel.Id,
                        CategoryId = cId
                    })
                    .ToList();

            originalProduct.ProductCategories = productCategories;
        }

        if (originalProduct is null)
        {
            throw new NullReferenceException(
                $"{nameof(originalProduct)} is null");
        }

        await _productRepo.Update(originalProduct);
        return originalProduct.ToDto().RequireNotNull();
    }

    public Task Delete(Guid id) => _productRepo.Delete(id);

    public Task<bool> Exists(Guid id) => _productRepo.Exists(id);

    public async Task<Result<int?>> IncrementClickedCount(Guid productId)
    {
        var product =
            await _productRepo.GetById(productId);

        if (product is null)
        {
            return new(new Exception($"Product {productId} doesn't exist"));
        }

        product.ClickCount++;

        await _productRepo.Update(product);

        return product.ClickCount;
    }

    public async Task<Result<int>> NewProductFavorite(
        Guid productId,
        string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return new(new Exception("Username is null or empty"));
        }
        
        var product =
            await _productRepo.GetById(productId);

        if (product is null)
        {
            return new(new Exception($"Product {productId} doesn't exist"));
        }

        var user =
            await _userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return new(new Exception($"User {userName} doesn't exist"));
        }

        var favorite =
            new Favorite
            {
                UserId    = user.Id,
                ProductId = product.Id
            };

        product.Favorites.Add(favorite);
        product.FavoriteCount++;

        await _productRepo.Update(product);
        return product.FavoriteCount;
    }
}