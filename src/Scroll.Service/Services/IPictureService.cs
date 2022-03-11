using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;

namespace Scroll.Service.Services;

public interface IPictureService
{
    Task<string> Add(string name, byte[] data);
    Task Delete(string name);
    Task DeleteFilesWithoutReference();
    Task<PagedList<PictureInfo>> Get(int pageIndex = 0, int pageSize = 10);
    Task<Picture?> Get(string name);
    Task<bool> Exists(string name);
}
