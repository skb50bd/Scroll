using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scroll.Library.Models.DTOs;
using Scroll.Service.Services;

namespace Scroll.Web.Areas.Admin.Products;

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

    public ProductFilterModel Filter { get; set; } = new();
    public List<ProductDto> Products { get; set; } = new();

    public async Task<ActionResult> OnGet()
    {
        return Page();
    }

    public async Task<ActionResult> OnPost(
        ProductFilterModel filter)
    {
        return Page();
    }
}
