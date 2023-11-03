using System.Diagnostics.Contracts;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Scroll.Api.Extensions;

public static class ResultExtensions
{
    [Pure]
    public static async Task<ActionResult<T>> MatchActionResult<T>(
        this Task<Result<T>> inputTask,
        Func<T, ActionResult<T>> success,
        Func<Exception, ActionResult<T>> fail
    )
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

    [Pure]
    public static async ValueTask<ActionResult<T>> MatchActionResult<T>(
        this ValueTask<Option<T>> inputTask,
        Func<T, ActionResult<T>> some,
        Func<ActionResult<T>> none
    )
    {
        var input = await inputTask;
        return input.Match(some, none);
    }

    [Pure]
    public static async Task<ActionResult<T>> MatchActionResult<T>(
        this Task<Option<T>> inputTask,
        Func<T, ActionResult<T>> some,
        Func<ActionResult<T>> none
    )
    {
        var input = await inputTask;
        return input.Match(some, none);
    }
}