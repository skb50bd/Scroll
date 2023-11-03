using Microsoft.AspNetCore.Components;
using Scroll.Web.Services;

namespace Scroll.Web.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private CategoryService CategoryService { get; set; } = default!;
    [Inject]
    private ProductService ProductService { get; set; } = default!;

    private PagedList<CategoryDto> categories = default!;
    private PagedList<ProductDto> products = default!;

    protected override async Task OnInitializedAsync()
    {
        categories =
            await CategoryService.GetCategories(pageSize: 4)
            ?? new PagedList<CategoryDto>();

        categories.Items = categories.Items
            .Take(4)
            .ToList();

        products =
            await ProductService.GetProductsAsync()
            ?? new PagedList<ProductDto>();

        foreach (var p in products.Items)
        {
            p.ImageName = "https://thecatapi.com/api/images/get?format=src&type=gif";
        }
    }
}