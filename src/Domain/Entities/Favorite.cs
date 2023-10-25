namespace Scroll.Domain.Entities;

public sealed class Favorite : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}