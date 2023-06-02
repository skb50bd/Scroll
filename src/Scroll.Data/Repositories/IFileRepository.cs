using Scroll.Library.Models.Entities;

namespace Scroll.Data.Repositories;

public interface IFileRepository
{
    Task Delete(string name);
    Task DeleteFilesWithoutReference();
    Task<ScrollFile?> Download(string fileName);
    Task<bool> Exists(string name);
    IQueryable<ScrollFileInfo> GetAll();
    Task Upload(FileInfo fileInfo, string contentType);
    Task Upload(Stream stream, string fileName, string contentType);
    Task Upload(string filePath, string fileName, string contentType);
}