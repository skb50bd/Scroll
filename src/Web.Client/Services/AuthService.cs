using System.Net.Http.Json;
using LanguageExt;
using Scroll.Domain.InputModels;

namespace Scroll.Web.Client.Services;

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