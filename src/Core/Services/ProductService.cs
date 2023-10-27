﻿using FluentValidation;
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
using static Scroll.Core.Services.ProductSortOrder;

namespace Scroll.Core.Services;

public class ProductService(
        IRepository<Product> productRepo,
        IRepository<ProductCategoryMapping> productCategoryMappingRepo,
        IValidator<ProductEditModel> productValidator
    ) : IProductService
{
    public Task<ProductDto?> Get(Guid id, CancellationToken token) =>
        productRepo
            .Table
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id, token)
            .ToDtoAsync();

    public Task<ProductDto?> GetByTitle(string title, CancellationToken token) =>
        productRepo
            .Table
            .Include(p => p.Categories)
            .Where(p => p.Title == title)
            .FirstOrDefaultAsync(token)
            .ToDtoAsync();

    public Task<ProductEditModel?> GetForEdit(Guid id, CancellationToken token) =>
        productRepo
            .Table
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id, token)
            .ToEditModelAsync();

    public async Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = IdDesc,
        Guid? categoryId = null,
        CancellationToken token = default
    )
    {
        var query =
            productRepo.Table;

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
                .ToPagedList(pageIndex, pageSize, token);

        return products.ProjectToDto();
    }

    public async Task<Result<ProductDto>> Insert(ProductEditModel editModel, CancellationToken token)
    {
        var validationResult =
            await productValidator.ValidateAsync(editModel, token);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var entity = editModel.ToEntity().RequireNotNull();
        editModel.CategoryIds ??= [];

        var productCategories =
            editModel.CategoryIds
                .Select(cId => new ProductCategoryMapping
                {
                    ProductId  = editModel.Id,
                    CategoryId = cId
                })
                .ToList();

        entity.ProductCategories = productCategories;

        await productRepo.Add(entity, token);
        return entity.ToDto().RequireNotNull();
    }

    public async Task<Result<ProductDto>> Update(ProductEditModel editModel, CancellationToken token)
    {
        var validationResult =
            await productValidator.ValidateAsync(editModel, token);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var originalProduct =
            await productRepo.Table
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == editModel.Id, token);

        if (originalProduct is null)
        {
            return new(new ArgumentException("Invalid Update Operation. Entity doesn't exist"));
        }

        editModel.ToEntity(originalProduct);

        var originalCategoryIds =
            originalProduct.ProductCategories
                .Select(pc => pc.CategoryId)
                .ToHashSet();

        editModel.CategoryIds ??= [];

        var removedCategoryIds =
            originalCategoryIds
                .Except(editModel.CategoryIds)
                .ToHashSet();

        if (removedCategoryIds.Count is not 0)
        {
            var removedProductCategories =
                await productCategoryMappingRepo.Table
                        .Where(pc =>
                            removedCategoryIds.Contains(pc.CategoryId)
                            && pc.ProductId == editModel.Id)
                        .ToListAsync(token);

            foreach (var pcMap in removedProductCategories)
            {
                await productCategoryMappingRepo.Delete(pcMap, token);
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

        await productRepo.Update(originalProduct, token);
        return originalProduct.ToDto().RequireNotNull();
    }

    public Task Delete(Guid id, CancellationToken token) => productRepo.Delete(id, token);

    public Task<bool> Exists(Guid id, CancellationToken token) => productRepo.Exists(id, token);

    public async Task<Result<int?>> IncrementClickedCount(Guid productId, CancellationToken token)
    {
        var product =
            await productRepo.GetById(productId, token);

        if (product is null)
        {
            return new(new Exception($"Product {productId} doesn't exist"));
        }

        product.ClickCount++;

        await productRepo.Update(product, token);

        return product.ClickCount;
    }

    public async Task<Result<int>> NewProductFavorite(
        Guid userId,
        Guid productId,
        CancellationToken token
    )
    {
        var product =
            await productRepo.GetById(productId, token);

        if (product is null)
        {
            return new(new ProductNotFound(productId));
        }

        if (await ProductIsLikedByUser(productId, userId, token))
        {
            return new(new DuplicateFavorite(userId, productId));
        }

        var favorite =
            new Favorite
            {
                UserId    = userId,
                ProductId = product.Id
            };

        product.Favorites.Add(favorite);
        product.FavoriteCount++;

        await productRepo.Update(product, token);
        return product.FavoriteCount;
    }

    public async Task<Result<int>> UndoProductFavorite(
        Guid userId,
        Guid productId,
        CancellationToken token
    )
    {
        var product =
            await productRepo.GetById(productId, token);

        if (product is null)
        {
            return new(new Exception($"Product {productId} doesn't exist"));
        }

        var maybeFavorite =
            await productRepo.Table
                .Where(p => p.Id == productId)
                .SelectMany(p => p.Favorites)
                .FirstOrDefaultAsync(f => f.UserId == userId, token);

        if (maybeFavorite is null)
        {
            return new(new FavoriteNotFound(userId, productId));
        }

        product.Favorites.Remove(maybeFavorite);
        product.FavoriteCount--;

        await productRepo.Update(product, token);
        return product.FavoriteCount;
    }

    public Task<bool> ProductIsLikedByUser(Guid productId, Guid userId, CancellationToken token) =>
        productRepo.Table
            .Where(p => p.Id == productId)
            .SelectMany(p => p.Favorites)
            .AnyAsync(f => f.UserId == userId, token);
}