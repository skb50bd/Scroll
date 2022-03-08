namespace Scroll.Library.Models.DTOs;

public record PictureInfo
{
    public string Name { get; init; }
    public long Size { get; init; }
    public string ContentType { get; init; }
    public DateTimeOffset? UploadedOn { get; init; }
}
