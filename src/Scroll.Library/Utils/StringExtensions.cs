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
        [MaybeNullWhen(true)]
        [NotNullWhen(false)]
        this string? str) =>
            string.IsNullOrWhiteSpace(str);

    public static bool IsNotBlank(
        [MaybeNullWhen(false)]
        [NotNullWhen(true)]
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
}
