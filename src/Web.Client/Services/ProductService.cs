using System.Net.Http.Json;
using LanguageExt.Common;

namespace Scroll.Web.Client.Services;

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
