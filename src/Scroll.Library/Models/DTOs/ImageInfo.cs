namespace Scroll.Library.Models.DTOs;

public record ImageInfo
{
    public int Id { get; init; }
    public string Name { get; init; }
    public long Size { get; init; }
    public DateTime UploadedOn { get; init; }
}