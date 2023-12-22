using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Scroll.Web.Client;
using Scroll.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddHttpClient(
        "API",
        configureClient: client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        }
    )
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
    .Services
    .AddScoped(sp =>
        sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")
    )
    .AddApiAuthorization()
    .Services
    .AddAuthorizationCore()
    .AddCascadingAuthenticationState()
    .AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>()
    .AddScoped<CategoryService>()
    .AddScoped<ProductService>()
    .AddScoped<AuthService>();

await builder.Build().RunAsync();
