using Scroll.Data;
using Scroll.Data.Repositories;
using Scroll.Domain;
using Scroll.Domain.Entities;

namespace Scroll.Core.Services;

public class PictureService(IFileRepository repo, IPictureProcessor processor) : IPictureService
{
    public async Task<string> Add(
        string name,
        byte[] data,
        int resizeToWidth = 1024,
        int resizeToHeight = 1024,
        CancellationToken token = default
    )
    {
        var tempFilePath = Path.GetTempFileName();

        await File.WriteAllBytesAsync(tempFilePath, data, token);

        var tempImageInfo =
            await data.ToTempFile(token);

        var convertedFile =
            await processor.ConvertToWebP(tempImageInfo, token);

        var resizedImageInfo =
            await processor.ResizeImage(
                convertedFile,
                resizeToWidth,
                resizeToHeight,
                token
            );

        // Tests show compression save no space
        // after webp conversion
        //var compressedImageInfo =
        //    _processor.CompressImage(resizedImageInfo);

        var nameWithoutExt =
            Path.GetFileNameWithoutExtension(name);

        var extension =
            Path.GetExtension(resizedImageInfo.Name);

        var fileName =
            nameWithoutExt + extension;

        await repo.Upload(
            filePath: resizedImageInfo.FullName,
            fileName: fileName,
            contentType: "image/webp",
            cancellationToken: token
        );

        return fileName;
    }

    public Task<ScrollFile?> Get(string name, CancellationToken token) =>
        repo.Download(name, token);

    public Task<PagedList<ScrollFileInfo>> Get(
                int pageIndex = 0,
                int pageSize = 10,
                CancellationToken token = default
            ) =>
        repo.GetAll().ToPagedList(pageIndex, pageSize, token);

    public Task Delete(string name, CancellationToken token) =>
        repo.Delete(name, token);

    public Task<bool> Exists(string name, CancellationToken token) =>
        repo.Exists(name, token);

    public Task DeleteFilesWithoutReference(CancellationToken token) =>
        repo.DeleteFilesWithoutReference(token);
}