using LanguageExt;
using LanguageExt.Common;
using System.Net.Http.Json;
using Scroll.Domain.InputModels;

namespace Scroll.Web.Services;

public class CategoryService(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _client = clientFactory.CreateClient("API");

    public async Task<PagedList<CategoryDto>?> GetCategories(
        int pageIndex           = 0,
        int pageSize            = 40,
        string filerString      = ""
    )
    {
        Console.WriteLine("Client Base Address: " + _client.BaseAddress);
        var result =
            await _client.GetFromJsonAsync<PagedList<CategoryDto>>(
                $"/categories?pageIndex={pageIndex}&pageSize={pageSize}&filterString={filerString}"
            );

        return result;
    }

    public async Task<Result<CategoryDto>> GetCategoryAsync(Guid id)
    {
        try
        {
            return
                await _client.GetFromJsonAsync<CategoryDto>($"/categories/{id}")
                ?? new Result<CategoryDto>(new Exception("Category not found"));
        }
        catch (Exception ex)
        {
            return new Result<CategoryDto>(ex);
        }
    }
}

public class ProductService(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _client = clientFactory.CreateClient("API");

    public async Task<PagedList<ProductDto>?> GetProductsAsync() =>
        await _client.GetFromJsonAsync<PagedList<ProductDto>>("/products");

    public async Task<Result<List<ProductDto>>> GetProductsAsync(Guid categoryId)
    {
        try
        {
            return
                await _client.GetFromJsonAsync<List<ProductDto>>($"/products/{categoryId}")
                ?? new Result<List<ProductDto>>(new Exception("Products not found"));
        }
        catch (Exception ex)
        {
            return new Result<List<ProductDto>>(ex);
        }
    }
}

public class AuthService(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _client = clientFactory.CreateClient("API");

    public async Task<Option<int>> LoginAsync(LoginModel request)
    {
        var response = await _client.PostAsJsonAsync("/account/login", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<int>();
            return result;
        }

        return 0;
    }
}