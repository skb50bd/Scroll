using System.Diagnostics.Contracts;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Scroll.Core.Extensions;

public static class ResultExtensions
{
    [Pure]
    public static async Task<TResult> MatchAsync<TSuccess, TResult>(
        this Task<Result<TSuccess>> inputTask,
        Func<TSuccess, TResult> success,
        Func<Exception, TResult> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

    [Pure]
    public static async Task<ActionResult<T>> MatchAsync<T>(
        this Task<Result<T>> inputTask,
        Func<T, ActionResult<T>> success,
        Func<Exception, ActionResult<T>> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

    [Pure]
    public static async Task<ActionResult<TResult>> MatchAsync<TSuccess, TResult>(
        this Task<Result<TSuccess>> inputTask,
        Func<TSuccess, ActionResult<TResult>> success,
        Func<Exception, ActionResult<TResult>> fail)
    {
        var input = await inputTask;
        return input.Match(success, fail);
    }

    [Pure]
    public static async Task<Result<T>> MatchAsync<T>(
        this Task<T?> inputTask,
        Func<T, Result<T>> success,
        Func<Result<T>> fail)
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
        Func<Result<T>> fail)
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