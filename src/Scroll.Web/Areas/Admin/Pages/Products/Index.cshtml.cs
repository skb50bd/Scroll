using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public IndexModel(IProductService productService,
        ICategoryService categoryService)
    {
        _productService  = productService;
        _categoryService = categoryService;
    }

    [BindProperty]
    public string? SearchString { get; set; } = string.Empty;

    [BindProperty]
    public int? CategoryId { get; set; }

    [BindProperty]
    public ProductSortOrder SortBy { get; set; } = ProductSortOrder.IdDesc;

    [BindProperty]
    public int PageIndex { get; set; } = 0;

    [BindProperty]
    public int PageSize { get; set; } = 40;

    public PagedList<CategoryDto> Categories { get; set; } = new();

    public PagedList<ProductDto> Products { get; set; } = new();

    public async Task<ActionResult> OnGetAsync()
    {
        Categories =
            await _categoryService.GetPaged(0, int.MaxValue);

        Products =
            await _productService.GetPaged(
                pageIndex: PageIndex,
                pageSize: PageSize,
                searchString: SearchString,
                sortBy: SortBy,
                categoryId: CategoryId);

        return Page();
    }
}