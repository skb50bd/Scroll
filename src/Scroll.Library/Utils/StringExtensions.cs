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
            str.IsNotBlank() is false;

    public static bool IsNotBlank(
        [MaybeNullWhen(false)]
        [NotNullWhen(true)]
        this string? str) =>
            string.IsNullOrWhiteSpace(str);
}
