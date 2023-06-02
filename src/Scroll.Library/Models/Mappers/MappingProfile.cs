using AutoMapper;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;

namespace Scroll.Library.Models.Mappers;

public class MappingProfile: Profile
{
    public MappingProfile() : base()
    {
        CreateMap<Product, ProductEditModel>()
            .ForMember(
                dest => dest.CategoryIds,
                opt  => opt.MapFrom(src =>
                            src.ProductCategories
                               .Select(c => c.CategoryId)
                               .ToList()));

        CreateMap<ProductEditModel, Product>()
            .ForMember(
                dest => dest.AddedOn,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.ClickCount,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.FavoriteCount,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.Favorites,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.Categories,
                opt => opt.Ignore());

        CreateMap<Product, ProductDto>();
        CreateMap<PagedList<Product>, PagedList<ProductDto>>();

        CreateMap<Category, CategoryEditModel>();
        CreateMap<CategoryEditModel, Category>()
            .ForMember(
                dest => dest.Products,
                opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();
        CreateMap<PagedList<Category>, PagedList<CategoryDto>>();

        CreateMap<ProductCategoryMapping, ProductCategoryMappingDto>();
    }
}