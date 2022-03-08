using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public class ImageRepository : IImageRepository
{
    private const string ImageContainerName = "scrollimages";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;
    private readonly IRepository<Product> _productsRepository;

    public ImageRepository(
        BlobServiceClient blobServiceClient,
        IRepository<Product> productsRepository)
    {
        _blobServiceClient =
            blobServiceClient;

        _containerClient =
            _blobServiceClient
                .GetBlobContainerClient(ImageContainerName);

        _productsRepository =
            productsRepository;
    }

    public async Task Upload(string filePath, string fileName)
    {
        var blobClient =
            _containerClient.GetBlobClient(fileName);

        await blobClient
            .UploadAsync(
                filePath,
                new BlobHttpHeaders
                {
                    ContentType = filePath.GetContentType()
                });
    }

    public Task Upload(FileInfo fileInfo) =>
        Upload(fileInfo.FullName, fileInfo.Name);

    public async Task Upload(Stream stream, string fileName)
    {
        var blobClient =
            _containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(stream);
    }

    public async Task<Picture?> Download(string fileName)
    {
        var blobClient =
            _containerClient.GetBlobClient(fileName);

        var downloadInfo =
            await blobClient.DownloadAsync();

        if (downloadInfo is null)
        {
            return null;
        }

        var picture =
            new Picture(
                downloadInfo.Value.Content,
                fileName,
                downloadInfo.Value.Details.ContentType,
                downloadInfo.Value.Details.ContentLength);

        return picture;
    }

    public async Task<bool> Exists(string name)
    {
        var blobClient =
            _containerClient.GetBlobClient(name);

        return await blobClient.ExistsAsync();
    }

    public async Task Delete(string name)
    {
        var blobClient =
            _containerClient.GetBlobClient(name);

        await blobClient.DeleteIfExistsAsync();
    }

    private static PictureInfo ConvertToPictureInfo(BlobItem blobItem) =>
        new()
        {
            Name        = blobItem.Name,
            ContentType = blobItem.Properties.ContentType,
            UploadedOn  = blobItem.Properties.CreatedOn,
            Size        = blobItem.Properties.ContentLength ?? 0
        };

    public async IAsyncEnumerable<PictureInfo> GetAll()
    {
        await foreach (var blobItem in _containerClient.GetBlobsAsync())
        {
            yield return ConvertToPictureInfo(blobItem);
        }
    }

    public async Task DeleteFilesWithoutReference()
    {
        var referencedImageNames =
            await _productsRepository
                .GetAll()
                .Select(p => p.ImageName)
                .ToListAsync();

        var referencedImageNamesSet =
            referencedImageNames.ToHashSet();

        await foreach (var picture in GetAll())
        {
            if (referencedImageNamesSet.Contains(picture.Name) is false
                && picture.UploadedOn < DateTimeOffset.UtcNow.AddDays(-1))
            {
                await Delete(picture.Name);
            }
        }
    }
}