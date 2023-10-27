using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Repositories;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Validators;

public class CategoryModelValidator : AbstractValidator<CategoryEditModel>
{
    public CategoryModelValidator(IRepository<Category> repository)
    {
        RuleFor(_ => _.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

        RuleFor(_ => _.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters long")
            .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters");

        RuleFor(_ => _)
            .MustAsync(async (category, ctx) =>
            {
                var nameAlreadyExists =
                    await repository.Table.AnyAsync(
                        _ => _.Name == category.Name && _.Id != category.Id,
                        ctx
                    );

                return nameAlreadyExists is false;
            })
            .WithMessage("Category Name Must be Unique");
    }
}