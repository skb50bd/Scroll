using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.Entities;
using Scroll.Service.Data;

namespace Scroll.Service.Services;

public class ImageService
{
    //private readonly ImageRepository _repo;
    //private readonly ImageProcessingService _processor;

    //public ImageService(
    //    ImageRepository repo,
    //    ImageProcessingService processor)
    //{
    //    _repo      = repo;
    //    _processor = processor;
    //}

    //public async Task<string> Add(string name, byte[] data)
    //{
    //    var tempFilePath = Path.GetTempFileName();

    //    await File.WriteAllBytesAsync(tempFilePath, data);

    //    var tempImageInfo =
    //        await data.ToTempFile();

    //    var convertedFile =
    //        await _processor.ConvertToWebP(tempImageInfo);

    //    var resizedImageInfo =
    //        await _processor.ResizeImage(convertedFile);

    //    var compressedImageInfo =
    //        _processor.CompressImage(resizedImageInfo);

    //    var nameWithoutExt =
    //        Path.GetFileNameWithoutExtension(name);

    //    var extension =
    //        Path.GetExtension(compressedImageInfo.Name);

    //    var image =
    //        await _repo.Upload(
    //            new Image
    //            {
    //                Name = nameWithoutExt + extension,
    //                Data = await compressedImageInfo.GetBytes()
    //            });

    //    return image.Name;
    //}

    //public Task<Image?> Get(string name) =>
    //    _repo.Download(name);

    //public Task<PagedList<ImageInfo>> Get(
    //    int pageIndex = 0,
    //    int pageSize = 10) =>
    //        _repo.GetAll(pageIndex, pageSize);

    //public Task Delete(string name) =>
    //    _repo.Delete(name);

    //public Task DeleteFilesWithoutReference() =>
    //    _repo.DeleteFilesWithoutReference();
}