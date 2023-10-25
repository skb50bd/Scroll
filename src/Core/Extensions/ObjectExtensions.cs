using System.Runtime.CompilerServices;

namespace Scroll.Core.Extensions;

public static class ObjectExtensions
{
    public static T RequireNotNull<T>(
        this T? obj,
        [CallerArgumentExpression(nameof(obj))]string? paramName = null
    ) where T : class
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return obj;
    }
}