namespace Scroll.Domain.DTOs;

public record ProductCategoryMappingDto
{
    public Guid ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}