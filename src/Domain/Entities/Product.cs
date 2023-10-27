namespace Scroll.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTimeOffset AddedOn { get; set; } = DateTimeOffset.UtcNow;
    public Uri? Link { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int ClickCount { get; set; }
    public List<Favorite> Favorites { get; set; } = [];
    public int FavoriteCount { get; set; }
    public List<ProductCategoryMapping> ProductCategories { get; set; } = [];
    public List<Category> Categories { get; set; } = [];
}