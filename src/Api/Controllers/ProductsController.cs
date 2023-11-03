using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scroll.Api.Extensions;
using Scroll.Core.Services;
using Scroll.Core.Validators;
using Scroll.Domain.Exceptions;
using Scroll.Domain.InputModels;

namespace Scroll.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(
    IProductService productService
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<ProductDto>>> Get(
        int pageIndex = 0,
        int pageSize = 40,
        string filerString = "",
        CancellationToken token = default
    )
    {
        var products =
            await productService.GetPaged(
                pageIndex,
                pageSize,
                filerString,
                token: token
            );

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public Task<ActionResult<ProductDto>> Get(Guid id, CancellationToken token) =>
        productService.Get(new(id), token)
            .MatchActionResult<ProductDto>(
                dto => Ok(dto),
                () => NotFound()
            );

    [HttpGet("Title/{title}")]
    public Task<ActionResult<ProductDto>> GetByTitle(string title, CancellationToken token) =>
        productService.GetByTitle(title, token)
            .MatchActionResult(
                dto => Ok(dto),
                () => NotFound()
            );

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public Task<ActionResult<ProductDto>> Post(ProductEditModel product, CancellationToken token) =>
        productService.Insert(product, token)
            .MatchActionResult(
                dto => CreatedAtAction(nameof(Get), new { id = dto.Id }, dto),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                }
            );

    [Authorize(Policy = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Put(Guid id, ProductEditModel product, CancellationToken token)
    {
        if (id != product.Id)
        {
            return Conflict("Route id and model id do not match.");
        }

        return await productService
            .Update(product, token)
            .MatchActionResult(
                _ => NoContent(),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                }
            );
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await productService.Delete(new(id), token);
        return NoContent();
    }

    [Authorize]
    [HttpPost("Favorite/{productId:guid}")]
    public async Task<ActionResult<int>> Favorite(Guid productId, CancellationToken token)
    {
        var maybeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (maybeUserId is null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(maybeUserId);
        return await productService
            .NewProductFavorite(
                userId    : userId,
                productId : new(productId),
                token     : token
            )
            .MatchActionResult(
                _ => NoContent(),
                exn => exn switch
                {
                    DuplicateFavorite ex => Conflict(ex.Message),
                    _                    => BadRequest(exn.Message)
                }
            );
    }

    [Authorize]
    [HttpPost("UndoFavorite/{productId:guid}")]
    public async Task<ActionResult<int>> UndoFavorite(Guid productId, CancellationToken token)
    {
        var maybeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (maybeUserId is null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(maybeUserId);
        return await productService
            .UndoProductFavorite(
                userId    : userId,
                productId : new(productId),
                token     : token
            )
            .MatchActionResult(
                count => Ok(count),
                exn => exn switch
                {
                    FavoriteNotFound ex  => NotFound(ex.Message),
                    _                    => BadRequest(exn.Message)
                }
            );
    }
}