using Scroll.Domain;
using Scroll.Domain.Entities;

namespace Scroll.Core.Services;

public interface IPictureService
{
    Task<string> Add(
        string name,
        byte[] data,
        int resizeToWidth = 1024,
        int resizeToHeight = 1024);

    Task Delete(string name);

    Task DeleteFilesWithoutReference();

    Task<PagedList<ScrollFileInfo>> Get(int pageIndex = 0, int pageSize = 10);

    Task<ScrollFile?> Get(string name);

    Task<bool> Exists(string name);
}