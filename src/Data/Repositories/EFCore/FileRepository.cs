using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories.EFCore;

public class FileRepository(
        ILogger<FileRepository> logger,
        AppDbContext dbContext
    ) : IFileRepository
{
    private readonly ILogger<FileRepository> _logger = logger;
    private readonly AppDbContext _dbContext = dbContext;

    public IQueryable<ScrollFileInfo> GetAll() =>
        _dbContext.Set<ScrollFileInfo>();

    public async Task Upload(
        FileInfo fileInfo,
        string contentType,
        CancellationToken cancellationToken = default
    )
    {
        if (fileInfo is not { Exists: true })
        {
            throw new ArgumentException(
                "The provided fileInfo is null or the file does not exist."
            );
        }

        await using var sourceFileStream = fileInfo.OpenRead();
        await Upload(sourceFileStream, fileInfo.Name, contentType, cancellationToken);
    }

    public async Task Upload(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default
    )
    {
        await using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream, cancellationToken);
        await SaveFile(
            fileName,
            (int)fileStream.Length,
            contentType,
            memoryStream.ToArray(),
            cancellationToken
        );
    }

    public async Task Upload(
        string sourceFilePath,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default
    )
    {
        await using var sourceFileStream =
            new FileStream(
                sourceFilePath,
                FileMode.Open,
                FileAccess.Read
        );

        await Upload(
            sourceFileStream,
            fileName,
            contentType,
            cancellationToken
        );
    }

    private async Task SaveFile(
        string fileName,
        int fileSize,
        string contentType,
        byte[] content,
        CancellationToken cancellationToken = default
    )
    {
        var scrollFile = new ScrollFile
        {
            Id          = Domain.FileId.New(),
            Name        = fileName,
            ContentType = contentType,
            Size        = fileSize,
            AddedOn     = DateTimeOffset.UtcNow,
            Content     = content
        };

        _dbContext.Set<ScrollFile>().Add(scrollFile);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ScrollFile?> Download(
            string fileName,
            CancellationToken cancellationToken
        ) =>
        await _dbContext
            .Set<ScrollFile>()
            .FirstOrDefaultAsync(
                sf => sf.Name == fileName,
                cancellationToken
            );

    public async Task<bool> Exists(
        string fileName,
        CancellationToken cancellationToken
    )
    {
        var scrollFile =
            await _dbContext
                .Set<ScrollFileInfo>()
                .FirstOrDefaultAsync(
                    sf => sf.Name == fileName,
                    cancellationToken
                );

        return scrollFile is not null;
    }

    public async Task Delete(
        string fileName,
        CancellationToken cancellationToken = default
    )
    {
        var scrollFile =
            await _dbContext
                .Set<ScrollFileInfo>()
                .FirstOrDefaultAsync(
                    sf => sf.Name == fileName,
                    cancellationToken
                );

        if (scrollFile is null)
        {
            _logger.LogInformation(
                "The file '{fileName}' was not found in the database.",
                fileName
            );

            return;
        }

        _dbContext.Remove(scrollFile);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFilesWithoutReference(CancellationToken cancellationToken)
    {
        var referencedImageNames =
            await _dbContext
                .Set<Product>()
                .Select(p => p.ImageName)
                .ToListAsync(cancellationToken);

        var referencedImageNamesSet =
            referencedImageNames.ToHashSet();

        var unReferencedFiles =
            await _dbContext
                .Set<ScrollFileInfo>()
                .Where(sf => referencedImageNamesSet.Contains(sf.Name) == false)
                .Where(sf => sf.AddedOn < DateTimeOffset.UtcNow.AddDays(-1))
                .ToListAsync(cancellationToken);

        _dbContext.RemoveRange(unReferencedFiles);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}