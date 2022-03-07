using ImageMagick;
using ImageMagick.Formats;
using Microsoft.Extensions.Logging;

namespace Scroll.Service.Services;

public class ImageProcessingService
{
    private readonly ILogger<ImageProcessingService> _logger;

    public ImageProcessingService(
        ILogger<ImageProcessingService> logger)
    {
        _logger = logger;
    }

    public FileInfo CompressImage(FileInfo fileInfo)
    {
        var originalSize = fileInfo.Length;

        var optimizer =
            new ImageOptimizer
            {
                OptimalCompression       = true,
                IgnoreUnsupportedFormats = true
            };

        optimizer.Compress(fileInfo);
        fileInfo.Refresh();

        var optimizedSize = fileInfo.Length;

        _logger.LogDebug(
            $"Compression Saved {originalSize - optimizedSize} bytes");

        return fileInfo;
    }

    public async Task<FileInfo> ResizeImage(
        FileInfo fileInfo,
        int widthRequest = 1024,
        int heightRequest = 1024)
    {
        using var image =
            new MagickImage(fileInfo);

        if (image.Width <= widthRequest
            && image.Height <= heightRequest)
        {
            return fileInfo;
        }

        var targetWidth =
            Math.Min(image.Width, widthRequest);

        var targetHeight =
            Math.Min(image.Height, heightRequest);

        var size =
            new MagickGeometry(targetWidth, targetHeight)
            {
                IgnoreAspectRatio = false
            };

        var originalSize =
            fileInfo.Length;

        image.Resize(size);

        await image.WriteAsync(fileInfo);

        fileInfo.Refresh();

        var resizedSize =
            fileInfo.Length;

        _logger.LogDebug(
            $"Resize Saved {originalSize - resizedSize} bytes");

        return fileInfo;
    }

    public async Task<FileInfo> ConvertToWebP(FileInfo fileInfo)
    {
        var originalExt =
            Path.GetExtension(fileInfo.Extension)
                .ToLowerInvariant();

        if (originalExt is ".webp")
        {
            return fileInfo;
        }

        using var image =
            new MagickImage(fileInfo);

        var defines =
            new WebPWriteDefines
            {
                NearLossless = 90,
                Method       = 6
            };

        var newFileName =
            Path.Combine(
                Path.GetDirectoryName(fileInfo.FullName) ?? string.Empty,
                Path.GetFileNameWithoutExtension(fileInfo.FullName) + ".webp");

        var newFileInfo =
            new FileInfo(newFileName);

        await image.WriteAsync(newFileInfo, defines);

        _logger.LogDebug(
            $"WebP Conversion Saved {fileInfo.Length - newFileInfo.Length} bytes");

        return newFileInfo;
    }
}