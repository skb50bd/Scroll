using LanguageExt;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Scroll.Data.Repositories;
using Scroll.Domain;
using Scroll.Domain.Entities;

public class MinioFileRepository(IMinioClient client) : IFileRepository
{
    public Task Delete(string name, CancellationToken cancellationToken) =>
        client.RemoveObjectAsync(
            new RemoveObjectArgs().WithObject(name),
            cancellationToken
        );

    public Task DeleteFilesWithoutReference(CancellationToken cancellationToken)
    {
        // You'll need to define how you track references to files to implement this.
        throw new NotImplementedException();
    }

    public async Task<Option<ScrollFile>> Download(string fileName, CancellationToken cancellationToken)
    {
        await using var stream = new MemoryStream();
        var args =
            new GetObjectArgs()
                .WithObject(fileName)
                .WithCallbackStream(x => x.CopyTo(stream));

        await client.GetObjectAsync(args, cancellationToken);

        if (stream.Length is 0)
        {
            return Option<ScrollFile>.None;
        }

        var newFile = new ScrollFile
        {
            Id          = FileId.New(),
            Name        = fileName,
            Content     = stream.ToArray(),
            Size        = stream.Length,
            ContentType = "application/octet-stream"
        };

        return newFile;
    }

    public async Task<bool> Exists(string name, CancellationToken cancellationToken)
    {
        try
        {
            await client.StatObjectAsync(
                new StatObjectArgs().WithObject(name),
                cancellationToken
            )
            .ConfigureAwait(false);

            return true;
        }
        catch (MinioException)
        {
            return false;
        }
    }

    public IQueryable<ScrollFileInfo> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Upload(FileInfo fileInfo, string contentType, CancellationToken cancellationToken)
    {
        await using var fileStream = fileInfo.OpenRead();
        await client.PutObjectAsync(
            new PutObjectArgs()
                .WithObject(fileInfo.Name)
                .WithStreamData(fileStream)
                .WithContentType(contentType),
            cancellationToken
        )
        .ConfigureAwait(false);
    }

    public async Task Upload(Stream stream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        await client.PutObjectAsync(
            new PutObjectArgs()
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithContentType(contentType),
            cancellationToken
        )
        .ConfigureAwait(false);
    }

    public async Task Upload(string filePath, string fileName, string contentType, CancellationToken cancellationToken)
    {
        await using var fileStream = new FileStream(filePath, FileMode.Open);
        await client.PutObjectAsync(
            new PutObjectArgs()
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithContentType(contentType),
            cancellationToken: cancellationToken
        )
        .ConfigureAwait(false);
    }
}
