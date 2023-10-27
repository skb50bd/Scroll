namespace Scroll.Core.Services;

public static class ImageFileExtensions
{
    public async static Task<FileInfo> ToTempFile(
        this byte[] bytes,
        CancellationToken token
    )
    {
        var tempFilePath =
            Path.GetTempFileName();

        await File.WriteAllBytesAsync(
            path: tempFilePath,
            bytes: bytes,
            cancellationToken: token
        );

        return new FileInfo(tempFilePath);
    }

    public static Task<byte[]> GetBytes(
        this FileInfo fileInfo,
        CancellationToken token
    ) => File.ReadAllBytesAsync(fileInfo.FullName, token);
}