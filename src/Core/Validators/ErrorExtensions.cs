using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scroll.Domain.Exceptions;

namespace Scroll.Core.Validators;

public static class ErrorExtensions
{
    private const string UnprocessableEntityDesc =
        "https://www.rfc-editor.org/rfc/rfc4918#section-11.2";

    public static ValidationProblemDetails ToProblemDetails(
        this ValidationException exception
    )
    {
        var error = new ValidationProblemDetails
        {
            Type   = UnprocessableEntityDesc,
            Status = StatusCodes.Status422UnprocessableEntity
        };

        foreach (var validationFailure in exception.Errors)
        {
            if (error.Errors.TryGetValue(validationFailure.PropertyName, out string[]? value))
            {
                error.Errors[validationFailure.PropertyName] =
                    [
                        .. value,
                        .. new[] { validationFailure.ErrorMessage }
                    ];
            }
            else
            {
                error.Errors.Add(new(
                    validationFailure.PropertyName,
                    [validationFailure.ErrorMessage]
                ));
            }
        }

        return error;
    }

    public static ValidationProblemDetails ToProblemDetails(
        this IdentityException exception
    )
    {
        var error = new ValidationProblemDetails
        {
            Type   = UnprocessableEntityDesc,
            Status = StatusCodes.Status422UnprocessableEntity
        };

        foreach (var identityError in exception.Errors)
        {
            if (error.Errors.TryGetValue(identityError.Code, out string[]? value))
            {
                error.Errors[identityError.Code] =
                    [.. value, .. new[] { identityError.Description }];
            }
            else
            {
                error.Errors.Add(new(
                    identityError.Code,
                    [identityError.Description]
                ));
            }
        }

        return error;
    }
}