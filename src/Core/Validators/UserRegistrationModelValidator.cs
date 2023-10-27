using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Repositories;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Validators;

public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
{
    public UserRegistrationModelValidator(IUserRepository repo)
    {
        RuleFor(_ => _.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email address is required")
            .MustAsync(async (email, ctx) =>
            {
                var emailAlreadyExists =
                    await repo.Table.AnyAsync(_ => _.Email == email, ctx);

                return emailAlreadyExists is false;
            }).WithMessage("Email already exists");

        RuleFor(_ => _.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

        RuleFor(_ => _.FullName)
            .MaximumLength(100);

        RuleFor(_ => _.UserName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50)
            .MustAsync(async (username, ctx) =>
            {
                var usernameExists =
                    await repo.Table.AnyAsync(_ => _.UserName == username, ctx);

                return usernameExists is false;
            })
            .WithMessage("Username already exists");
    }
}