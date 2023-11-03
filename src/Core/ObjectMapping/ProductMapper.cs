using LanguageExt;
using Riok.Mapperly.Abstractions;
using Scroll.Common.Extensions;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Core.ObjectMapping;

[Mapper(UseReferenceHandling = true)]
public static partial class ProductMapper
{
    public static partial ProductDto? ToDto(this Product? entity);

    private static Option<ProductEditModel> MapToEditModel(this Option<Product> entity) =>
        entity.Match(
            Some: e =>
            {
                var dto = e.ToEditModel().RequireNotNull();
                dto.CategoryIds = CategoriesToCategoryIds(e.Categories);
                return dto;
            },
            None: () => Option<ProductEditModel>.None
        );

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

    public static async Task<Option<ProductDto>> ToDtoAsync(this Task<Product?> source)
    {
        var srcObject = await source;
        return srcObject.ToDto();
    }

    public static async ValueTask<Option<ProductDto>> ToDtoAsync(this ValueTask<Option<Product>> source)
    {
        var srcObject = await source;
        return srcObject.Map(ToDto).Map(x => x!);
    }

    public static async Task<Option<ProductDto>> ToDtoAsync(this Task<Option<Product>> source)
    {
        var srcObject = await source;
        return srcObject.Map(ToDto).Map(x => x!);
    }

    public static async Task<Option<ProductEditModel>> ToEditModelAsync(this Task<Product?> source)
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