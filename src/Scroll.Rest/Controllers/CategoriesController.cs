using Microsoft.AspNetCore.Mvc;
using Scroll.Core.Services;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.Entities;

namespace Scroll.Rest.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }
    
    // GET
    [HttpGet(Name = "GetCategories")]
    public async Task<PagedList<CategoryDto>> Get(
        int pageIndex = 0,
        int pageSize = 40,
        string filerString = ""
    )
    {
        return await _categoryService.GetPaged(pageIndex, pageSize, filerString);
    }
}