using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public PagedList<CategoryDto> Categories = new();

    public string? Filter { get; set; }

    public async Task<ActionResult> OnGet(
        int pageIndex = 0,
        int pageSize = 40,
        string? filter = null)
    {
        Filter = filter;
        ViewData["filter"] = filter;

        Categories =
            await _categoryService
                .GetPaged(
                    pageIndex,
                    pageSize,
                    filter);

        return Page();
    }
}