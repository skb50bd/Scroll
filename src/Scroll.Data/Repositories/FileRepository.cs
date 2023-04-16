using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public class FileRepository : IFileRepository
{
    private readonly ILogger<FileRepository> _logger;
    
    private const string RootPath = "/app/files";
    
    private readonly IEntityRepository<Product> _productsRepository;
    private readonly IEntityRepository<ScrollFile> _fileRepository;
    private readonly IEntityRepository<ScrollFileInfo> _fileInfoRepository;

    public FileRepository(
        IEntityRepository<Product> productsRepository,
        IEntityRepository<ScrollFile> scrollFileRepository, 
        ILogger<FileRepository> logger, 
        IEntityRepository<ScrollFileInfo> fileInfoRepository)
    {
        _productsRepository = productsRepository;
        _fileRepository     = scrollFileRepository;
        _fileInfoRepository = fileInfoRepository;
        _logger             = logger;
    }

    public IQueryable<ScrollFileInfo> GetAll() =>
        _fileInfoRepository.GetAll();

    public async Task Upload(FileInfo fileInfo, string contentType)
    {
        if (fileInfo is not { Exists: true })
        {
            throw new ArgumentException(
                "The provided fileInfo is null or the file does not exist."
            );
        }

        var fileName       = fileInfo.Name;
        var targetFilePath = Path.Combine(RootPath, fileName);

        await using var sourceFileStream = fileInfo.OpenRead();
        await using var targetFileStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);
        await sourceFileStream.CopyToAsync(targetFileStream);
        await SaveFile(fileName, targetFilePath, contentType);
    }
    
    public async Task Upload(Stream fileStream, string fileName, string contentType)
    {
        var filePath = Path.Combine(RootPath, fileName);
        await using var targetFileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(targetFileStream);
        await SaveFile(fileName, filePath, contentType);
    }

    public async Task Upload(string sourceFilePath, string fileName, string contentType)
    {
        var targetFilePath = Path.Combine(RootPath, fileName);
        await using var sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        await using var targetFileStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);
        await sourceFileStream.CopyToAsync(targetFileStream);
        await SaveFile(fileName, targetFilePath, contentType);
    }

    private async Task SaveFile(string filePath, string fileName, string contentType)
    {
        var fileSize = new FileInfo(filePath).Length;
        var scrollFile = new ScrollFile
        {
            Name        = fileName,
            Path        = filePath,
            ContentType = contentType,
            Size        = fileSize,
            AddedOn     = DateTimeOffset.UtcNow,
        };

        await _fileRepository.Add(scrollFile);
    }
    
    public async Task<ScrollFile?> Download(string fileName)
    {
        var scrollFile = 
            await _fileRepository
                .GetAll()
                .FirstOrDefaultAsync(sf => sf.Name == fileName);

        if (scrollFile is null)
        {
            return null;
        }

        if (File.Exists(scrollFile.Path) is false)
        {
            throw new FileNotFoundException(
                $"The file '{fileName}' was not found at the expected location."
            );
        }

        await using var fileStream = 
            new FileStream(
                scrollFile.Path, 
                FileMode.Open, 
                FileAccess.Read
            );

        scrollFile.Content = await File.ReadAllBytesAsync(scrollFile.Path);
        return scrollFile;
    }

    public async Task<bool> Exists(string fileName)
    {
        var scrollFile = 
            await _fileRepository
                .GetAll()
                .FirstOrDefaultAsync(sf => sf.Name == fileName);

        if (scrollFile is null)
        {
            return false;
        }

        if (File.Exists(scrollFile.Path) is false)
        {
            throw new FileNotFoundException(
                $"The file '{fileName}' was not found at the expected location."
            );
        }

        return true;
    }

    public async Task Delete(string fileName)
    {
        var scrollFile = 
            await _fileRepository
                .GetAll()
                .FirstOrDefaultAsync(sf => sf.Name == fileName);

        if (scrollFile is null)
        {
            _logger.LogInformation(
                "The file '{fileName}' was not found in the database.",
                fileName
            );
            
            return;
        }
        
        if (File.Exists(scrollFile.Path))
        {
            File.Delete(scrollFile.Path);
        }
        else
        {
            _logger.LogWarning(
                "The file '{fileName}' was not found at the expected location.",
                fileName
            );
        }

        await _fileRepository.Delete(scrollFile);
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

        var unReferencedFiles =
            await _fileRepository
                .GetAll()
                .Where(sf => referencedImageNamesSet.Contains(sf.Name) == false)
                .Where(sf => sf.AddedOn < DateTimeOffset.UtcNow.AddDays(-1))
                .ToListAsync();

        await _fileRepository.Delete(unReferencedFiles);
    }
}