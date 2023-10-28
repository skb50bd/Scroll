namespace Scroll.Domain.Entities;

public sealed class Category : IEntity<CategoryId>
{
    public CategoryId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ProductCategoryMapping> ProductCategories { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}
