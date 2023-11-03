namespace Scroll.Common.DTOs;

public record CategoryDto
{
    public CategoryId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}