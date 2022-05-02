using System.ComponentModel.DataAnnotations;
using Scroll.Library.Utils;
using Scroll.Service.Services;

namespace Scroll.Web.Services;

public class PictureUploadService
{
    private readonly IPictureService _pictureService;

    public PictureUploadService(IPictureService pictureService)
    {
        _pictureService = pictureService;
    }
    
    private readonly HashSet<string> _extensions =
        new()
        {
            ".jpg",
            ".jpeg",
            ".gif",
            ".png",
            ".webp"
        };

    public async Task<string> UploadPicture(PictureUploadModel input)
    {
        if (input.HasFile is false)
        {
            throw new NullReferenceException(
                $"{input.File} is empty");
        }

        await using var ms =
            new MemoryStream();

        var extension =
            Path.GetExtension(input.File!.FileName);

        if (_extensions.Contains(extension) is false)
        {
            throw new InvalidDataException(
                $"File with extension \"{extension}\" not supported.");
        }

        await input.File.CopyToAsync(ms);

        var fileName =
            await _pictureService.Add(
                input.Name!.ToUrlString(),
                ms.ToArray(),
                input.Width,
                input.Height);

        return fileName;
    }
}

public class PictureUploadModel
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