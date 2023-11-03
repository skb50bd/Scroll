namespace Scroll.Common.DTOs;

public record ProductCategoryMappingDto
{
    public ProductId ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public CategoryId CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}