namespace Scroll.Library.Models.Entities;

public class ScrollFileInfo : Entity
{
    public required string Name { get; init; }
    public required string Path { get; init; }
    public required long Size { get; init; }
    public DateTimeOffset AddedOn { get; init; } = DateTimeOffset.UtcNow;
    public required string ContentType { get; init; } = string.Empty;
}

public class ScrollFile : ScrollFileInfo
{
    public byte[] Content { get; set; } = Array.Empty<byte>();
}