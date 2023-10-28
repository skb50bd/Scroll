using System.ComponentModel.DataAnnotations;

namespace Scroll.Domain.InputModels;

public record class ProductEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public List<CategoryId>? CategoryIds { get; set; } = [];

    [DataType(DataType.Url)]
    public string? Link { get; set; }

    [Display(Name = "Image Name")]
    public string? ImageName { get; set; } = string.Empty;
}