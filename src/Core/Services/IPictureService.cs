using Scroll.Domain;
using Scroll.Domain.Entities;

namespace Scroll.Core.Services;

public interface IPictureService
{
    Task<string> Add(
        string name,
        byte[] data,
        int resizeToWidth = 1024,
        int resizeToHeight = 1024,
        CancellationToken token = default
    );

    Task Delete(string name, CancellationToken token);

    Task DeleteFilesWithoutReference(CancellationToken token);

    Task<PagedList<ScrollFileInfo>> Get(int pageIndex = 0, int pageSize = 10, CancellationToken token = default);

    Task<ScrollFile?> Get(string name, CancellationToken token);

    Task<bool> Exists(string name, CancellationToken token);
}