namespace Scroll.Domain.DTOs;

public sealed record ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTimeOffset AddedOn { get; set; } = DateTimeOffset.UtcNow;
    public string? Link { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int ClickCount { get; set; }
    public int FavoriteCount { get; set; }
    public ComparableList<CategoryDto> Categories { get; set; } = new();
}