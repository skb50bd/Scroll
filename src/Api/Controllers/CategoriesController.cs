using FluentValidation;
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
            int pageIndex      = 0,
            int pageSize       = 40,
            string filerString = ""
        ) =>
            categoryService.GetPaged(
                    pageIndex,
                    pageSize,
                    filerString
            );

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto?>> Get(Guid id)
    {
        var category = await categoryService.Get(id);
        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<CategoryDto?>> GetByName(string name)
    {
        var category = await categoryService.GetByName(name);
        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public Task<ActionResult<CategoryDto>> Post(CategoryEditModel model) =>
        categoryService
            .Insert(model)
            .MatchAsync(
                dto => CreatedAtAction(nameof(Get), new { id = dto.Id }, dto),
                exn => exn switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    _ => throw StandardErrors.Unreachable
                }
            );

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> Put(Guid id, CategoryEditModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        return await categoryService.Update(model)
            .MatchAsync(
                dto => NoContent(),
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
        await categoryService.Delete(id);
        return NoContent();
    }
}