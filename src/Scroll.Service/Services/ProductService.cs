using Microsoft.AspNetCore.Identity;
using Scroll.Data;
using Scroll.Library.Models;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
using Scroll.Library.Models.Mappers;
using Scroll.Service.Data;

namespace Scroll.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepo;
    private readonly UserManager<AppUser> _userManager;

    public ProductService(
        IRepository<Product> productRepo,
        UserManager<AppUser> userManager)
    {
        _productRepo = productRepo;
        _userManager = userManager;
    }

    public Task<Product?> Get(int id) =>
        _productRepo.Get(id);

    public Task<PagedList<Product>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40) =>
            _productRepo
                .GetAll()
                .ToPagedList(pageIndex, pageSize);

    public async Task<Product> Insert(ProductEditModel editModel)
    {
        var entity =
            editModel.ToEntity();

        await _productRepo.Upsert(entity);

        return entity;
    }

    public async Task<Product> Update(ProductEditModel editModel)
    {
        var original =
            await Get(editModel.Id);

        if (original is null)
        {
            throw new ArgumentException(
                "Invalid Update Operation. Entity doesn't exist");
        }

        var updated =
            editModel.ToEntity(original);

        await _productRepo.Upsert(updated);

        return updated;
    }

    public Task<bool> Delete(int id) =>
        _productRepo.Delete(id);

    public Task<bool> Exists(int id) =>
        _productRepo.Exists(id);

    public async Task<int?> IncrementClickedCount(int productId)
    {
        var product =
            await Get(productId);

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