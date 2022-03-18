using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scroll.Data;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
using Scroll.Library.Utils;
using Scroll.Service.Data;
using static Scroll.Service.Services.ProductSortOrder;

namespace Scroll.Service.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository<Product> _productRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepository<ProductCategoryMapping> _productCategoryMappingRepo;

    public ProductService(
        IEntityRepository<Product> productRepo,
        UserManager<AppUser> userManager,
        IMapper mapper,
        IRepository<ProductCategoryMapping> productCategoryMappingRepo)
    {
        _productRepo                = productRepo;
        _productCategoryMappingRepo = productCategoryMappingRepo;
        _userManager                = userManager;
        _mapper                     = mapper;
    }

    public async Task<ProductDto?> Get(int id) =>
        _mapper.Map<ProductDto?>(
            await _productRepo.Get(id));

    public async Task<ProductDto?> GetByTitle(string title) =>
        _mapper.Map<ProductDto?>(
            await _productRepo
                    .GetAll()
                    .Where(p => p.Title == title)
                    .FirstOrDefaultAsync());

    public async Task<ProductEditModel?> GetForEdit(int id) =>
        _mapper.Map<ProductEditModel?>(
            await _productRepo
                    .GetAll()
                    .Include(p => p.ProductCategories)
                    .FirstOrDefaultAsync(p => p.Id == id));

    public async Task<PagedList<ProductDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = IdDesc,
        int? categoryId = null)
    {
        var query =
            _productRepo.GetAll();

        if (categoryId > 0)
        {
            query =
                query.Where(c =>
                    c.Categories.Any(c =>
                        c.Id == categoryId));
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

        return _mapper.Map<PagedList<ProductDto>>(products);
    }

    public async Task<ProductDto> Insert(ProductEditModel editModel)
    {
        var entity =
            _mapper.Map<Product>(editModel);

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

        await _productRepo.Upsert(entity);

        return _mapper.Map<ProductDto>(entity);
    }

    public async Task<ProductDto> Update(ProductEditModel editModel)
    {
        var originalProduct =
            await _productRepo.GetAll()
                    .Include(p => p.ProductCategories)
                    .FirstOrDefaultAsync(p => p.Id == editModel.Id);

        if (originalProduct is null)
        {
            throw new ArgumentException(
                "Invalid Update Operation. Entity doesn't exist");
        }

        var updatedProduct =
            _mapper.Map(editModel, originalProduct);

        var originalCategoryIds =
            originalProduct.ProductCategories
                .Select(pc => pc.CategoryId);

        editModel.CategoryIds ??= new();

        var removedCategoryIds =
            originalCategoryIds
                .Except(editModel.CategoryIds);

        if (removedCategoryIds.Any())
        {
            var removedProductCategries =
                await _productCategoryMappingRepo
                    .GetAll()
                    .Where(pc =>
                        removedCategoryIds.Contains(pc.CategoryId)
                        && pc.ProductId == editModel.Id)
                    .ToListAsync();

            foreach (var pcMap in removedProductCategries)
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

            updatedProduct.ProductCategories = productCategories;
        }

        if (updatedProduct is null)
        {
            throw new NullReferenceException(
                $"{nameof(updatedProduct)} is null");
        }

        await _productRepo.Upsert(updatedProduct);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public Task<bool> Delete(int id) =>
        _productRepo.Delete(id);

    public Task<bool> Exists(int id) =>
        _productRepo.Exists(id);

    public async Task<int?> IncrementClickedCount(int productId)
    {
        var product =
            await _productRepo.Get(productId);

        if (product is null)
        {
            return null;
        }

        product.ClickCount++;

        await _productRepo.Upsert(product);

        return product.ClickCount;
    }

    public async Task<int> NewProductFavorite(
        int productId,
        string userName)
    {
        var product =
            await _productRepo.Get(productId);

        if (product is null)
        {
            throw new ArgumentException(
                $"Product {productId} doesn't exist");
        }

        var user =
            await _userManager.FindByNameAsync(userName);

        if (user is null)
        {
            throw new ArgumentException(
                $"User {userName} doesn't exist");
        }

        var favorite =
            new Favorite
            {
                UserId    = user.Id,
                ProductId = product.Id
            };

        product.Favorites.Add(favorite);

        product.FavoriteCount++;

        await _productRepo.Upsert(product);

        return product.FavoriteCount;
    }
}