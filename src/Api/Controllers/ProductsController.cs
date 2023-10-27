using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scroll.Core.Extensions;
using Scroll.Core.Services;
using Scroll.Core.Validators;
using Scroll.Domain;
using Scroll.Domain.DTOs;
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
    public async Task<ActionResult<ProductDto?>> Get(Guid id, CancellationToken token)
    {
        var product = await productService.Get(id, token);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("Title/{title}")]
    public async Task<ActionResult<ProductDto?>> GetByTitle(string title, CancellationToken token)
    {
        var product = await productService.GetByTitle(title, token);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public Task<ActionResult<ProductDto>> Post(ProductEditModel product, CancellationToken token) =>
        productService.Insert(product, token)
            .MatchAsync(
                dto => CreatedAtAction(nameof(Get), new { id = dto.Id }, dto),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                },
                token
            );

    [Authorize(Policy = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Put(Guid id, ProductEditModel product, CancellationToken token)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        return await productService
            .Update(product, token)
            .MatchAsync(
                _ => NoContent(),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                },
                token
            );
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await productService.Delete(id, token);
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
                productId : productId,
                token     : token
            )
            .MatchAsync(
                _ => NoContent(),
                exn => exn switch
                {
                    DuplicateFavorite ex => Conflict(ex.Message),
                    _                    => BadRequest(exn.Message)
                },
                token
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
                productId : productId,
                token     : token
            )
            .MatchAsync(
                _ => NoContent(),
                exn => exn switch
                {
                    FavoriteNotFound ex  => NotFound(ex.Message),
                    _                    => BadRequest(exn.Message)
                },
                token
            );
    }
}