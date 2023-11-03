namespace Scroll.Common.DTOs;

public class Picture(
    Stream stream,
    string name,
    string contentType,
    long size
)
{
    public Stream Stream { get; init; } = stream;
    public string Name { get; init; } = name;
    public string ContentType { get; init; } = contentType;
    public long Size { get; } = size;
}