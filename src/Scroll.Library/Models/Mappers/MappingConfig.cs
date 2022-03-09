using AutoMapper;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;

namespace Scroll.Library.Models.Mappers;

public class MappingConfig: Profile
{
    public MappingConfig() : base()
    {
        CreateMap<Product, ProductEditModel>();
        CreateMap<ProductEditModel, Product>();
        CreateMap<Product, ProductDto>();

        CreateMap<Category, CategoryEditModel>();
        CreateMap<CategoryEditModel, Category>();
        CreateMap<Category, CategoryDto>();
    }
}