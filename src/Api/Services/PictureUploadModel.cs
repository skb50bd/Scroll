using System.ComponentModel.DataAnnotations;

namespace Scroll.Api.Services;

public sealed class PictureUploadModel
{
    public IFormFile? File { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [Display(Name = "Image Name")]
    public string? Name { get; set; }

    [Range(8, 5000)]
    [Display(Name = "Max Width")]
    public int Width { get; set; } = 1024;

    [Range(8, 5000)]
    [Display(Name = "Max Height")]
    public int Height { get; set; } = 1024;

    public bool HasFile => File?.Length > 0;
}