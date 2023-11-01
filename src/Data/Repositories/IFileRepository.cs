using LanguageExt;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IFileRepository
{
    Task Delete(string name, CancellationToken cancellationToken);
    Task DeleteFilesWithoutReference(CancellationToken cancellationToken);
    Task<Option<ScrollFile>> Download(string fileName, CancellationToken cancellationToken);
    Task<bool> Exists(string name, CancellationToken cancellationToken);
    IQueryable<ScrollFileInfo> GetAll();
    Task Upload(FileInfo fileInfo, string contentType, CancellationToken cancellationToken);
    Task Upload(Stream stream, string fileName, string contentType, CancellationToken cancellationToken);
    Task Upload(string filePath, string fileName, string contentType, CancellationToken cancellationToken);
}