using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Data;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public class FileRepository : IFileRepository
{
    private readonly ILogger<FileRepository> _logger;
    private readonly ScrollDbContext _dbContext;

    public FileRepository(
        ILogger<FileRepository> logger, 
        ScrollDbContext dbContext)
    {
        _logger    = logger;
        _dbContext = dbContext;
    }

    public IQueryable<ScrollFileInfo> GetAll() =>
        _dbContext.Set<ScrollFileInfo>();

    public async Task Upload(FileInfo fileInfo, string contentType)
    {
        if (fileInfo is not { Exists: true })
        {
            throw new ArgumentException(
                "The provided fileInfo is null or the file does not exist."
            );
        }

        await using var sourceFileStream = fileInfo.OpenRead();
        await Upload(sourceFileStream, fileInfo.Name, contentType);
    }
    
    public async Task Upload(Stream fileStream, string fileName, string contentType)
    {
        await using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        await SaveFile(fileName, (int)fileStream.Length, contentType, memoryStream.ToArray());
    }

    public async Task Upload(string sourceFilePath, string fileName, string contentType)
    {
        await using var sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        await Upload(fileName, fileName, contentType);
    }

    private async Task SaveFile(
        string fileName, 
        int fileSize, 
        string contentType,
        byte[] content)
    {
        var scrollFile = new ScrollFile
        {
            Name        = fileName,
            ContentType = contentType,
            Size        = fileSize,
            AddedOn     = DateTimeOffset.UtcNow,
            Content     = content
        };

        _dbContext.Set<ScrollFile>().Add(scrollFile);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<ScrollFile?> Download(string fileName) =>
        await _dbContext
            .Set<ScrollFile>()
            .FirstOrDefaultAsync(sf => sf.Name == fileName);

    public async Task<bool> Exists(string fileName)
    {
        var scrollFile = 
            await _dbContext
                .Set<ScrollFileInfo>()
                .FirstOrDefaultAsync(sf => sf.Name == fileName);

        return scrollFile is not null;
    }

    public async Task Delete(string fileName)
    {
        var scrollFile = 
            await _dbContext
                .Set<ScrollFileInfo>()
                .FirstOrDefaultAsync(sf => sf.Name == fileName);

        if (scrollFile is null)
        {
            _logger.LogInformation(
                "The file '{fileName}' was not found in the database.",
                fileName
            );
            
            return;
        }

        _dbContext.Remove(scrollFile);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteFilesWithoutReference()
    {
        var referencedImageNames =
            await _dbContext
                .Set<Product>()
                .Select(p => p.ImageName)
                .ToListAsync();

        var referencedImageNamesSet =
            referencedImageNames.ToHashSet();

        var unReferencedFiles =
            await _dbContext
                .Set<ScrollFileInfo>()
                .Where(sf => referencedImageNamesSet.Contains(sf.Name) == false)
                .Where(sf => sf.AddedOn < DateTimeOffset.UtcNow.AddDays(-1))
                .ToListAsync();

        _dbContext.RemoveRange(unReferencedFiles);
        await _dbContext.SaveChangesAsync();
    }
}