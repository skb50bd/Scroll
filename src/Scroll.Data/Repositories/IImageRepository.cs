using Scroll.Library.Models.DTOs;

namespace Scroll.Service.Data;

public interface IImageRepository
{
    Task Delete(string name);
    Task DeleteFilesWithoutReference();
    Task<Picture?> Download(string fileName);
    Task<bool> Exists(string name);
    IAsyncEnumerable<PictureInfo> GetAll();
    Task Upload(FileInfo fileInfo);
    Task Upload(Stream stream, string fileName);
    Task Upload(string filePath, string fileName);
}