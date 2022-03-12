namespace Scroll.Library.Models.Entities;

public class ProductCategoryMapping
{
    public int ProductId { get; set; }
    public virtual Product?  Product { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
