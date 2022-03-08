﻿using Scroll.Data;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Data;

namespace Scroll.Service.Services;

public class PictureService : IPictureService
{
    private readonly IImageRepository _repo;
    private readonly IPictureProcessor _processor;

    public PictureService(
        IImageRepository repo,
        IPictureProcessor processor)
    {
        _repo      = repo;
        _processor = processor;
    }

    public async Task<string> Add(string name, byte[] data)
    {
        var tempFilePath = Path.GetTempFileName();

        await File.WriteAllBytesAsync(tempFilePath, data);

        var tempImageInfo =
            await data.ToTempFile();

        var convertedFile =
            await _processor.ConvertToWebP(tempImageInfo);

        var resizedImageInfo =
            await _processor.ResizeImage(convertedFile);

        var compressedImageInfo =
            _processor.CompressImage(resizedImageInfo);

        var nameWithoutExt =
            Path.GetFileNameWithoutExtension(name);

        var extension =
            Path.GetExtension(compressedImageInfo.Name);

        var fileName =
            nameWithoutExt + extension;

        await _repo.Upload(
            filePath: compressedImageInfo.FullName,
            fileName);

        return fileName;
    }

    public Task<Picture?> Get(string name) =>
        _repo.Download(name);

    public Task<PagedList<PictureInfo>> Get(
        int pageIndex = 0,
        int pageSize = 10) =>
            _repo.GetAll()
                .ToPagedList(
                    pageIndex,
                    pageSize);

    public Task Delete(string name) =>
        _repo.Delete(name);

    public Task DeleteFilesWithoutReference() =>
        _repo.DeleteFilesWithoutReference();
}