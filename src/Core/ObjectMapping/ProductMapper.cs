using Riok.Mapperly.Abstractions;
using Scroll.Core.Extensions;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Core.ObjectMapping;

[Mapper(UseReferenceHandling = true)]
public static partial class ProductMapper
{
    public static partial ProductDto? ToDto(this Product? entity);

    private static ProductEditModel? MapToEditModel(this Product? entity)
    {
        if (entity is null)
        {
            return default;
        }

        var dto = entity.ToEditModel().RequireNotNull();
        dto.CategoryIds = CategoriesToCategoryIds(entity.Categories);
        return dto;
    }

    [MapperIgnoreTarget(nameof(ProductEditModel.CategoryIds))]
    private static partial ProductEditModel? ToEditModel(this Product? entity);

    private static List<CategoryId>? CategoriesToCategoryIds(this List<Category>? categories) =>
        categories
            ?.Select(c => c.Id)
            .ToList();

    [MapperIgnoreTarget(nameof(Product.AddedOn))]
    [MapperIgnoreTarget(nameof(Product.ClickCount))]
    [MapperIgnoreTarget(nameof(Product.FavoriteCount))]
    [MapperIgnoreTarget(nameof(Product.Favorites))]
    [MapperIgnoreTarget(nameof(Product.Categories))]
    public static partial Product? ToEntity(this ProductEditModel? editModel);

    [MapperIgnoreTarget(nameof(Product.AddedOn))]
    [MapperIgnoreTarget(nameof(Product.ClickCount))]
    [MapperIgnoreTarget(nameof(Product.FavoriteCount))]
    [MapperIgnoreTarget(nameof(Product.Favorites))]
    [MapperIgnoreTarget(nameof(Product.Categories))]
    public static partial void ToEntity(this ProductEditModel? editModel, Product entity);

    public static async Task<ProductDto?> ToDtoAsync(this ValueTask<Product?> source)
    {
        var srcObject = await source;
        return ToDto(srcObject);
    }

    public static async Task<ProductDto?> ToDtoAsync(this Task<Product?> source)
    {
        var srcObject = await source;
        return ToDto(srcObject);
    }

    public static async Task<ProductEditModel?> ToEditModelAsync(this ValueTask<Product?> source)
    {
        var srcObject = await source;
        return MapToEditModel(srcObject);
    }

    public static async Task<ProductEditModel?> ToEditModelAsync(this Task<Product?> source)
    {
        var srcObject = await source;
        return MapToEditModel(srcObject);
    }

    public static partial PagedList<ProductDto> ProjectToDto(this PagedList<Product> source);
}

[Mapper]
public static partial class ProductQueryableMapper
{
    public static partial IQueryable<ProductDto> ProjectToDto(this IQueryable<Product> source);
}