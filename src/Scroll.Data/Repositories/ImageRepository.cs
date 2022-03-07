namespace Scroll.Service.Data;

public class ImageRepository
{
    //private readonly IMongoDatabase _database;
    //private readonly GridFSBucket _bucket;

    //public ImageRepository(
    //    IMongoDatabase database)
    //{
    //    _database = database;

    //    _bucket =
    //        new GridFSBucket(
    //            database,
    //            MongoDbConfig.ImageBucketOptions);
    //}

    //public async Task<Image> Upload(Image image)
    //{
    //    var id =
    //        await _bucket
    //            .UploadFromBytesAsync(
    //                image.Name,
    //                image.Data);

    //    return image with { Id = id };
    //}

    //public async Task<Image?> Download(ObjectId id)
    //{
    //    var bytes =
    //        await _bucket.DownloadAsBytesAsync(id);

    //    if (bytes is not { Length: > 0 })
    //    {
    //        return null;
    //    }

    //    return new Image
    //    {
    //        Id   = id,
    //        Data = bytes
    //    };
    //}

    //public async Task<Image?> Download(string name)
    //{
    //    var bytes =
    //        await _bucket.DownloadAsBytesByNameAsync(name);

    //    var id =
    //        await GetIdFromFileName(name);

    //    if (id is null)
    //    {
    //        return null;
    //    }

    //    return new Image
    //    {
    //        Id   = id.Value,
    //        Name = name,
    //        Data = bytes
    //    };
    //}

    //public async Task<ObjectId?> GetIdFromFileName(string fileName)
    //{
    //    var filesCollection =
    //        _database.GetCollection<dynamic>(
    //            MongoDbConfig.ImageBucketOptions.BucketName
    //            + ".files");

    //    var filter =
    //        Builders<dynamic>.Filter
    //            .Eq("filename", fileName);

    //    var projection =
    //        Builders<dynamic>.Projection
    //            .Include("_id");

    //    var document =
    //        await filesCollection
    //            .Find(filter)
    //            .Project(projection)
    //            .FirstOrDefaultAsync();

    //    return document?[0].AsNullableObjectId;
    //}

    //public async Task<bool> Exists(string name) =>
    //    await GetIdFromFileName(name) is not null;

    //public async Task Delete(string name)
    //{
    //    var id =
    //        await GetIdFromFileName(name);

    //    await _bucket.DeleteAsync(id);
    //}

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