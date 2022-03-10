namespace Scroll.Library.Models.Entities;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual List<Product> Products { get; set; } = new();
}