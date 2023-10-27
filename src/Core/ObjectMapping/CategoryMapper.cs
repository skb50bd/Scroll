using Riok.Mapperly.Abstractions;
using Scroll.Domain;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Core.ObjectMapping;

[Mapper]
public static partial class CategoryMapper
{
    public static partial CategoryDto? ToDto(this Category? entity);
    public static partial CategoryEditModel? ToEditModel(this Category? entity);

    [MapperIgnoreTarget(nameof(Category.Products))]
    public static partial Category? ToEntity(this CategoryEditModel? editModel);

    [MapperIgnoreTarget(nameof(Category.Products))]
    public static partial void ToEntity(this CategoryEditModel? editModel, Category entity);

    public static partial IQueryable<CategoryDto> ProjectToDto(this IQueryable<Category> source);

    public static partial List<CategoryDto> ProjectToDto(this List<Category> source);

    public static partial PagedList<CategoryDto> ProjectToDto(this PagedList<Category> source);

    public static async Task<CategoryDto?> ToDtoAsync(this ValueTask<Category?> source)
    {
        var srcObject = await source;
        return ToDto(srcObject);
    }

    public static async Task<CategoryDto?> ToDtoAsync(this Task<Category?> source)
    {
        var srcObject = await source;
        return ToDto(srcObject);
    }

    public static async Task<CategoryEditModel?> ToEditModelAsync(this ValueTask<Category?> source)
    {
        var srcObject = await source;
        return ToEditModel(srcObject);
    }
}