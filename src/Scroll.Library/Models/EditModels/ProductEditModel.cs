using System.ComponentModel.DataAnnotations;

namespace Scroll.Library.Models.EditModels;

public class ProductEditModel
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

    [Required]
    [DataType(DataType.Url)]
    public string Link { get; set; } = string.Empty;

    [Required]
    public string ImageName { get; set; } = string.Empty;
}