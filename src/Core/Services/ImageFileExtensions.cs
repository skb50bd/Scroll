namespace Scroll.Core.Services;

public static class ImageFileExtensions
{
    public async static Task<FileInfo> ToTempFile(
        this byte[] bytes)
    {
        var tempFilePath =
            Path.GetTempFileName();

        await File.WriteAllBytesAsync(
            path: tempFilePath,
            bytes: bytes);

        return new FileInfo(tempFilePath);
    }

    public static Task<byte[]> GetBytes(
        this FileInfo fileInfo) =>
            File.ReadAllBytesAsync(fileInfo.FullName);
}