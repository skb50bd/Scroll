namespace Scroll.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ProductCategoryMapping> ProductCategories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}