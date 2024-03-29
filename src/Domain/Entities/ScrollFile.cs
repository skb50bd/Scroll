namespace Scroll.Domain.Entities;

public class ScrollFileInfo : IEntity<FileId>
{
    public required FileId Id { get; set; }
    public required string Name { get; init; }
    public required long Size { get; init; }
    public DateTimeOffset AddedOn { get; init; } = DateTimeOffset.UtcNow;
    public required string ContentType { get; init; } = string.Empty;
}

public class ScrollFile : ScrollFileInfo
{
    public byte[] Content { get; set; } = [];
}