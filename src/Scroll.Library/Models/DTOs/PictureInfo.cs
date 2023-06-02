namespace Scroll.Library.Models.DTOs;

public record PictureInfo
{
    public PictureInfo(
        string name,
        long size,
        string contentType,
        DateTimeOffset? uploadedOn)
    {
        Name        = name;
        Size        = size;
        ContentType = contentType;
        UploadedOn  = uploadedOn;
    }

    public string Name { get; init; }
    public long Size { get; init; }
    public string ContentType { get; init; }
    public DateTimeOffset? UploadedOn { get; init; }
}