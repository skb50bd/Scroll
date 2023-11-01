using System.Diagnostics.Contracts;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Scroll.Core.Extensions;

public static class ResultExtensions
{
    [Pure]
    public static async Task<TResult> MatchAsync<TSuccess, TResult>(
        this Task<Result<TSuccess>> inputTask,
        Func<TSuccess, TResult> success,
        Func<Exception, TResult> fail
    )
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

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
    
    //
    // [Pure]
    // public static async Task<ActionResult<T>> MatchAsync<T>(
    //     this Task<Option<T>> inputTask,
    //     Func<T, ActionResult<T>> some,
    //     Func<ActionResult<T>> none
    // )
    // {
    //     var input = await inputTask;
    //     return input.Match(some, none);
    // }

    // [Pure]
    // public static async Task<ActionResult> MatchActionResult<T>(
    //     this Task<Result<T>> inputTask,
    //     Func<T, ActionResult> success,
    //     Func<Exception, ActionResult> fail
    // )
    // {
    //     var input = await inputTask;
    //     return input.Match(success, fail);
    // }
    
    [Pure]
    public static async Task<Result<T>> MatchAsync<T>(
        this Task<T?> inputTask,
        Func<T, Result<T>> success,
        Func<Result<T>> fail
    )
    {
        var input = await inputTask;

        return input is null
            ? fail()
            : success(input);
    }

    [Pure]
    public static async Task<Result<T>> MatchAsync<T>(
        this Task<T?> inputTask,
        Func<T, Task<Result<T>>> success,
        Func<Result<T>> fail
    )
    {
        var input = await inputTask;

        return input is null
            ? fail()
            : await success(input);
    }

    [Pure]
    public static async Task<Result<TResult>> MapAsync<TSource, TResult>(
        this Task<Result<TSource>> srcTask,
        Func<TSource, Task<TResult>> f
    )
    {
        var src = await srcTask;
        return await src.MapAsync(f);
    }
}