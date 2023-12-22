using Microsoft.AspNetCore.Components;
using Scroll.Web.Client.Services;

namespace Scroll.Web.Client.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private CategoryService CategoryService { get; set; } = default!;
    [Inject]
    private ProductService ProductService { get; set; } = default!;

    private PagedList<CategoryDto> Categories = default!;
    private PagedList<ProductDto> Products = default!;

    protected override async Task OnInitializedAsync()
    {
        Categories =
            await CategoryService.GetCategories(pageSize: 10)
            ?? new PagedList<CategoryDto>();

        Products =
            await ProductService.GetProductsAsync()
            ?? new PagedList<ProductDto>();

        foreach (var p in Products.Items)
        {
            p.ImageName = "https://thecatapi.com/api/images/get?format=src&type=gif";
        }
    }
}