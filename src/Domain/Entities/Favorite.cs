namespace Scroll.Domain.Entities;

public sealed class Favorite
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ProductId ProductId { get; set; }
    public Product? Product { get; set; }
}