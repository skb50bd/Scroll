namespace Scroll.Common.DTOs;

public record PictureInfo(
    string Name,
    long Size,
    string ContentType,
    DateTimeOffset? UploadedOn
);