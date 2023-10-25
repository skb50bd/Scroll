namespace Scroll.Data.Repositories;

public static class FileExtensions
{
    public static string GetContentType(this string fileName)
    {
        return "application/octet-stream";
    }
}