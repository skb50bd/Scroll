using Microsoft.AspNetCore.Mvc;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Service.Services;

namespace Scroll.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryService productService)
    {
        _logger          = logger;
        _categoryService = productService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        var category =
            await _categoryService.Get(id);

        if (category is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(category);
        }
    }

    [HttpGet]
    public Task<PagedList<CategoryDto>> Get(
        int pageIndex = 0,
        int pageSize = 40,
        string? filterString = null) =>
            _categoryService.GetPaged(pageIndex, pageSize, filterString);

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Post(CategoryEditModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(model);
        }

        var product = await _categoryService.Insert(model);

        return product;
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDto>> Put(CategoryEditModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(model);
        }

        var product = await _categoryService.Update(model);

        return product;
    }

    [HttpDelete]
    public Task<bool> Delete(int categoryId) =>
        _categoryService.Delete(categoryId);
}