using LanguageExt.Common;
using System.Net.Http.Json;

namespace Scroll.Web.Client.Services;

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
