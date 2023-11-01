using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Scroll.Domain.Exceptions;

public static class StandardErrors
{
    public static InvalidCredentials InvalidCredentials { get; } = new();
    public static UnreachableException Unreachable { get; } = new();
}

public class InvalidCredentials : Exception { }

public class IdentityException(IEnumerable<IdentityError> errors) : Exception
{
    public List<IdentityError> Errors { get; private set; } = errors.ToList();
}

public class DuplicateFavorite(Guid userId, ProductId productId)
    : Exception($"User {userId} already favorited product {productId}")
{
    public Guid UserId { get; private init; } = userId;
    public ProductId ProductId { get; private init; } = productId;
}

public class FavoriteNotFound(Guid userId, ProductId productId)
    : Exception($"User {userId} has not favorited product {productId}")
{
    public Guid UserId { get; private init; } = userId;
    public ProductId ProductId { get; private init; } = productId;
}

public class ProductNotFound(ProductId productId)
    : Exception($"Product {productId} not found")
{
    public ProductId ProductId { get; private init; } = productId;
}

public class CategoryNotFound(CategoryId categoryId)
    : Exception($"Category {categoryId} not found")
{
    public CategoryId CategoryId { get; private init; } = categoryId;
}