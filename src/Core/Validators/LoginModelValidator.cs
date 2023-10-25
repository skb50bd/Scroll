using FluentValidation;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Validators;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(_ => _.UserName)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(_ => _.Password)
            .NotEmpty().WithMessage("Password cannot be empty");
    }
}