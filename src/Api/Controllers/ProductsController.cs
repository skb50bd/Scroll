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
        string filerString = ""
    )
    {
        var products =
            await productService.GetPaged(
                pageIndex,
                pageSize,
                filerString
            );

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto?>> Get(Guid id)
    {
        var product = await productService.Get(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("Title/{title}")]
    public async Task<ActionResult<ProductDto?>> GetByTitle(string title)
    {
        var product = await productService.GetByTitle(title);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public Task<ActionResult<ProductDto>> Post(ProductEditModel product) =>
        productService.Insert(product)
            .MatchAsync(
                dto => CreatedAtAction(nameof(Get), new { id = dto.Id }, dto),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                }
            );

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Put(Guid id, ProductEditModel product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        return await productService
            .Update(product)
            .MatchAsync(
                _ => NoContent(),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                }
            );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await productService.Delete(id);
        return NoContent();
    }

    [Authorize]
    [HttpPost("Favorite/{productId:guid}")]
    public async Task<ActionResult<int>> Favorite(Guid productId)
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrWhiteSpace(userName))
        {
            return Unauthorized();
        }

        return await productService
            .NewProductFavorite(productId, userName)
            .MatchAsync(
                _ => NoContent(),
                exn => BadRequest(exn.Message)
            );
    }

    [HttpPost("Click/{productId:guid}")]
    public Task<ActionResult<int?>> Click(Guid productId) =>
        productService.IncrementClickedCount(productId)
            .MatchAsync(
                _ => NoContent(),
                exn => BadRequest(exn.Message)
            );
}