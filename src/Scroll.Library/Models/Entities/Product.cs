namespace Scroll.Library.Models.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTimeOffset AddedOn { get; set; } = DateTimeOffset.Now;
    public Uri? Link { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int ClickCount { get; set; }
    public virtual List<Favorite> Favorites { get; set; } = new();
    public int FavoriteCount { get; set; }
    public virtual List<Category> Categories { get; set; } = new();
}