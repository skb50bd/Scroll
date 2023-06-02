using System.Diagnostics;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Scroll.Library.Utils;

public static class StringExtensions
{
    public static string ToSpaced(this string str) =>
        str.Humanize();

    public static string ToSpaced(this Enum input) =>
        input.Humanize(LetterCasing.Title);

    public static bool IsBlank(
        [MaybeNullWhen(true)] [NotNullWhen(false)]
        this string? str) =>
        string.IsNullOrWhiteSpace(str);

    public static bool IsNotBlank(
        [MaybeNullWhen(false)] [NotNullWhen(true)]
        this string? str) =>
        str.IsBlank() is false;

    public static string CleanLink(this string url) =>
        url.Replace("https://", "")
            .Replace("http://", "")
            .TrimEnd('/');

    public static string Glimpse(this string str, int length) =>
        str.Truncate(length, "...");

    public static string ToUrlString(this string str) =>
        str.Kebaberize();

    public static string GetContentTypeFromFileExtension(this string fileExtension) =>
        fileExtension switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".txt" => "text/plain",
            ".csv" => "text/csv",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            ".7z" => "application/x-7z-compressed",
            ".tar" => "application/x-tar",
            ".gz" => "application/gzip",
            ".wav" => "audio/wav",
            ".mp3" => "audio/mpeg",
            ".ogg" => "audio/ogg",
            ".wma" => "audio/x-ms-wma",
            ".mp4" => "video/mp4",
            ".mkv" => "video/x-matroska",
            ".mov" => "video/quicktime",
            ".wmv" => "video/x-ms-wmv",
            ".flv" => "video/x-flv",
            ".avi" => "video/x-msvideo",
            ".3gp" => "video/3gpp",
            ".3g2" => "video/3gpp2",
            ".m4a" => "audio/mp4",
            ".m4v" => "video/mp4",
            ".mpg" => "video/mpeg",
            ".mp2" => "video/mpeg",
            ".mpeg" => "video/mpeg",
            ".mpe" => "video/mpeg",
            ".mpv" => "video/mpeg",
            ".m2v" => "video/mpeg",
            ".m4b" => "audio/mp4",
            ".m4r" => "audio/mp4",
            ".flac" => "audio/flac",
            ".aac" => "audio/aac",
            ".webm" => "video/webm",
            ".swf" => "application/x-shockwave-flash",
            ".xml" => "application/xml",
            ".json" => "application/json",
            ".svg" => "image/svg+xml",
            ".html" => "text/html",
            ".htm" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".php" => "text/x-php",
            ".asp" => "text/asp",
            ".aspx" => "text/aspx",
            ".cshtml" => "text/cshtml",
            ".vbhtml" => "text/vbhtml",
            ".cs" => "text/x-csharp",
            ".vb" => "text/x-vb",
            ".c" => "text/x-c",
            ".cpp" => "text/x-c",
            ".h" => "text/x-c",
            ".hpp" => "text/x-c",
            ".java" => "text/x-java-source",
            ".py" => "text/x-python",
            ".rb" => "text/x-ruby",
            ".sh" => "text/x-shellscript",
            ".sql" => "text/x-sql",
            ".bat" => "text/x-dos-batch",
            ".cmd" => "text/x-dos-batch",
            ".ps1" => "text/x-powershell",
            ".psm1" => "text/x-powershell",
            ".psd1" => "text/x-powershell",
            ".ps1xml" => "text/x-powershell",
            ".psc1" => "text/x-powershell",
            ".psrc" => "text/x-powershell",
            ".pssc" => "text/x-powershell",
            ".md" => "text/markdown",
            ".markdown" => "text/markdown",
            ".mdown" => "text/markdown",
            ".mkd" => "text/markdown",
            ".mkdn" => "text/markdown",
            ".mkdown" => "text/markdown",
            ".ron" => "text/ron",
            ".toml" => "text/toml",
            ".yaml" => "text/yaml",
            ".yml" => "text/yaml",
            ".ini" => "text/ini",
            ".conf" => "text/plain",
            ".config" => "text/plain",
            ".rtf" => "application/rtf",
            _ => throw new UnreachableException()
        };
}