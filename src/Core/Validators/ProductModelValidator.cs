using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Repositories;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Validators;

public class ProductModelValidator : AbstractValidator<ProductEditModel>
{
    public ProductModelValidator(IRepository<Product> repository)
    {
        RuleFor(_ => _.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters long")
            .MaximumLength(50).WithMessage("Title cannot be longer than 50 characters");
        
        RuleFor(_ => _.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(5).WithMessage("Description must be at least 5 characters long")
            .MaximumLength(5000).WithMessage("Description cannot be longer than 5000 characters");

        RuleFor(_ => _.Price)
            .GreaterThanOrEqualTo(0m).WithMessage("Price cannot be negative");
        
        RuleFor(_ => _.Link)
            .MaximumLength(200).WithMessage("Link cannot be longer than 200 characters");
        
        RuleFor(_ => _.ImageName)
            .NotEmpty().WithMessage("Image name is required")
            .MaximumLength(100).WithMessage("Image name cannot be longer than 100 characters");

        RuleFor(_ => _)
            .MustAsync(async (product, ctx) =>
            {
                var titleAlreadyExists = 
                    await repository.Table.AnyAsync(
                        _ => _.Title == product.Title && _.Id != product.Id, 
                        ctx
                    );
                
                return titleAlreadyExists is false;
            })
            .WithMessage("Product Name Must be Unique");
    }
}