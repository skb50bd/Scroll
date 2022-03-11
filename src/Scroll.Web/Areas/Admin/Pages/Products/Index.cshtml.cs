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

    public string? SearchString { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public ProductSortOrder SortBy { get; set; } = ProductSortOrder.IdDesc;
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 40;

    public PagedList<CategoryDto> Categories { get; set; } = new();

    public PagedList<ProductDto> Products { get; set; } = new();

    public async Task<ActionResult> OnGetAsync(
        int pageIndex = 0,
        int pageSize = 40,
        string? searchString = null,
        ProductSortOrder sortBy = ProductSortOrder.IdDesc,
        int? categoryId = null)
    {
        PageIndex    = pageIndex;
        PageSize     = pageSize;
        SearchString = searchString;
        SortBy       = sortBy;
        CategoryId   = categoryId;

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