namespace Scroll.Domain.Entities;

public class ProductCategoryMapping
{
    public ProductId ProductId { get; set; }
    public virtual Product?  Product { get; set; }
    public CategoryId CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}