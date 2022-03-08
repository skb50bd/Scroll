
namespace Scroll.Service.Services
{
    public interface IPictureProcessor
    {
        FileInfo CompressImage(FileInfo fileInfo);
        Task<FileInfo> ConvertToWebP(FileInfo fileInfo);
        Task<FileInfo> ResizeImage(FileInfo fileInfo, int widthRequest = 1024, int heightRequest = 1024);
    }
}