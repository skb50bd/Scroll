using Scroll.Core.Services;
using Scroll.Domain.Utils;

namespace Scroll.Api.Services;

public class PictureUploadService(IPictureService pictureService)
{
    private readonly IPictureService _pictureService = pictureService;
    private readonly HashSet<string> _extensions =
        [
            ".jpg",
            ".jpeg",
            ".gif",
            ".png",
            ".webp"
        ];

    public async Task<string> UploadPicture(PictureUploadModel input)
    {
        if (input.HasFile is false)
        {
            throw new NullReferenceException(
                $"{input.File} is empty"
            );
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