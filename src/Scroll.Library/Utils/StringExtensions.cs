using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Scroll.Library.Utils;

public static class StringExtensions
{
    public static string ToSpaced(
        this string str) =>
            str.Humanize();

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

    public static string Glimpse(this string str, int length)
    {
        if (str.Length <= length)
        {
            return str;
        }

        return $"{str[..length].TrimEnd()}...";
    }
}
