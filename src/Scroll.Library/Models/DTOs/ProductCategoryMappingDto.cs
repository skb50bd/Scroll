namespace Scroll.Library.Models.DTOs;

public record ProductCategoryMappingDto
{
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}