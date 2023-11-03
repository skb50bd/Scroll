using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Scroll.Web.Components;
using Scroll.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddApiAuthorization(opts =>
// {
//     opts.AuthenticationPaths.RegisterPath      = "account/register";
//     opts.AuthenticationPaths.LogInPath         = "account/login";
//     opts.AuthenticationPaths.LogInCallbackPath = "account/login-callback";
//     opts.AuthenticationPaths.ProfilePath       = "security/profile";
// });
// builder.Services.AddAuthorizationCore();

builder.Services
    .AddHttpClient(
        "API",
        configureClient: client =>
        {
            client.BaseAddress = new Uri("http://localhost:8080");
        }
    );
    // .AddHttpMessageHandler(sp =>
    //     sp.GetService<AuthorizationMessageHandler>()
    //         ?.ConfigureHandler(
    //             authorizedUrls : new[] { "http://localhost:8080" },
    //             scopes         : new[] { "api" }
    //         )
    //     ?? throw new InvalidOperationException(
    //         "The authorization message handler must be registered in DI in order to use this overload."
    //     )
    // );

builder.Services.AddOptions();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
