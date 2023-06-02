using Microsoft.AspNetCore.StaticFiles;

namespace Scroll.Data.Repositories;

public static class FileExtensions
{
    private static readonly FileExtensionContentTypeProvider Provider = new();

    public static string GetContentType(this string fileName)
    {
        if (Provider.TryGetContentType(fileName, out var contentType) is false)
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}