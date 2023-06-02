﻿namespace Scroll.Library.Models.DTOs;

public class Picture
{
    public Picture(
        Stream stream,
        string name,
        string contentType,
        long size)
    {
        Stream      = stream;
        Name        = name;
        ContentType = contentType;
        Size        = size;
    }

    public Stream Stream { get; init; }
    public string Name { get; init; }
    public string ContentType { get; init; }
    public long Size { get; }
}