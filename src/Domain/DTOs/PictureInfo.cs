namespace Scroll.Domain.DTOs;

public record PictureInfo(string Name,
    long Size,
    string ContentType,
    DateTimeOffset? UploadedOn
);