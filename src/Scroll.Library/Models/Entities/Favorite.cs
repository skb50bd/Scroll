namespace Scroll.Library.Models.Entities;

public class Favorite
{
    public int UserId { get; set; }
    public virtual AppUser? User { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}