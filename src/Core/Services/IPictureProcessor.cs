namespace Scroll.Core.Services;

public interface IPictureProcessor
{
    FileInfo CompressImage(FileInfo fileInfo);
    Task<FileInfo> ConvertToWebP(FileInfo fileInfo, CancellationToken token);
    Task<FileInfo> ResizeImage(
        FileInfo fileInfo,
        int widthRequest = 1024,
        int heightRequest = 1024,
        CancellationToken token = default
    );
}
