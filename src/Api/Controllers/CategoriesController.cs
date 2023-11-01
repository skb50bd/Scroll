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
public class CategoriesController(
    ICategoryService categoryService
) : ControllerBase
{

    [HttpGet(Name = "GetCategories")]
    public Task<PagedList<CategoryDto>> Get(
            int pageIndex           = 0,
            int pageSize            = 40,
            string filerString      = "",
            CancellationToken token = default
        ) =>
            categoryService.GetPaged(
                    pageIndex,
                    pageSize,
                    filerString,
                    token
            );

    [HttpGet("{id:guid}")]
    public ValueTask<ActionResult<CategoryDto>> Get(Guid id, CancellationToken token) =>
        categoryService.Get(new(id), token)
            .MatchActionResult(
                dto => Ok(dto),
                ()  => NotFound()
            );

    [HttpGet("{name}")]
    public ValueTask<ActionResult<CategoryDto>> GetByName(string name, CancellationToken token) =>
        categoryService.GetByName(name, token)
            .MatchActionResult(
                dto => Ok(dto),
                () =>  NotFound()
            );

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public Task<ActionResult<CategoryDto>> Post(CategoryEditModel model, CancellationToken token) =>
        categoryService
            .Insert(model, token)
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
    public async Task<ActionResult<CategoryDto>> Put(Guid id, CategoryEditModel model, CancellationToken token)
    {
        if (id != model.Id)
        {
            return Conflict("Route id and model id do not match.");
        }

        return await 
            categoryService
                .Update(model, token)
                .MatchActionResult(
                    dto => NoContent(), 
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
        await categoryService.Delete(new(id), token);
        return NoContent();
    }
}