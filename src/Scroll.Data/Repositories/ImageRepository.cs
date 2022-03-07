using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Scroll.Library.Models.DTOs;

namespace Scroll.Service.Data;

public class ImageRepository
{
    private const string ImageContainerName = "ScrollImages";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;

    public ImageRepository(
        BlobServiceClient blobServiceClient)
    {
        _blobServiceClient =
            blobServiceClient;

        _containerClient =
            _blobServiceClient
                .GetBlobContainerClient(ImageContainerName);
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

    //public async Task<PagedList<ImageInfo>> GetAll(
    //    int pageIndex = 0,
    //    int pageSize = 10)
    //{
    //    var filesCollection =
    //        _database.GetCollection<dynamic>(
    //            MongoDbConfig.ImageBucketOptions.BucketName
    //            + ".files");

    //    var filter =
    //        Builders<dynamic>.Filter.Empty;

    //    var projection =
    //        Builders<dynamic>.Projection
    //            .Include("_id")
    //            .Include("length")
    //            .Include("uploadDate")
    //            .Include("filename");

    //    var query =
    //        filesCollection
    //            .Find(filter)
    //            .Project(projection);

    //    var totalCount =
    //        await query.CountDocumentsAsync();

    //    var documents =
    //        await query
    //            .Skip(pageSize * pageIndex)
    //            .Limit(pageSize)
    //            .ToListAsync();

    //    var files =
    //        documents
    //            .Select(doc => new ImageInfo
    //            {
    //                Id         = doc[0].AsObjectId,
    //                Size       = doc[1].AsInt64,
    //                UploadedOn = doc[2].AsBsonDateTime.ToLocalTime(),
    //                Name       = doc[3].AsString
    //            })
    //            .ToList();

    //    return new(
    //        files,
    //        pageSize,
    //        pageIndex,
    //        (int)totalCount);
    //}

    //public async Task DeleteFilesWithoutReference()
    //{
    //    var productCol =
    //        _database.GetCollection<Product>(nameof(Product));

    //    var referencedImageNames =
    //        await productCol
    //            .Find(Builders<Product>.Filter.Empty)
    //            .Project(Builders<Product>.Projection.Expression(p => p.ImageName))
    //            .ToListAsync();

    //    var referencedImageNamesSet =
    //        referencedImageNames.ToHashSet();

    //    var allImages =
    //        await GetAll(0, int.MaxValue);

    //    var allImageIdsWithoutRereference =
    //        allImages
    //            .Items
    //            .Where(i => referencedImageNamesSet.Contains(i.Name) is false)
    //            .Where(i => i.UploadedOn < DateTime.Now.AddDays(1))
    //            .Select(i => i.Id)
    //            .ToList();

    //    foreach (var id in allImageIdsWithoutRereference)
    //    {
    //        await _bucket.DeleteAsync(id);
    //    }
    //}
}