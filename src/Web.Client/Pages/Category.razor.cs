using Microsoft.AspNetCore.Components;
using Scroll.Common.Extensions;
using Scroll.Web.Client.Services;

namespace Scroll.Web.Client.Pages;

public partial class Category : ComponentBase
{
    [Inject] private CategoryService _categoryService { get; set; } = null!;
    [Inject] private ProductService _productService { get; set; } = null!;

    private CategoryDto? _category = default!;
    private PagedList<ProductDto>? _products;

    [Parameter]
    public Guid CategoryId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        _category = await _categoryService.GetCategoryAsync(CategoryId).MatchAsync(
            category => category,
            _        => null
        );
        // _products   = await _productService.GetProductsAsync();
    }
}