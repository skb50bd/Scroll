using System.ComponentModel.DataAnnotations;

namespace Scroll.Library.Models.EditModels;

public record class ProductEditModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [DataType(DataType.Url)]
    public string? Link { get; set; }

    [Required]
    [Display(Name = "Image Name")]
    public string ImageName { get; set; } = string.Empty;
}