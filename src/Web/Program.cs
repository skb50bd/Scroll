using Scroll.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Scroll.Web.Identity;
using Scroll.Core;
using Scroll.Data;
using Microsoft.AspNetCore.DataProtection;
using Scroll.Domain.Entities;
using Scroll.Web.Client.Services;
using Scroll.Web.Client.Layout;
using Scroll.Web.Client;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureServices()
    .AddHttpContextAccessor()
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .Services
    .AddCascadingAuthenticationState()
    .AddScoped<UserAccessor>()
    .AddScoped<IdentityRedirectManager>()
    .AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>()
    .AddDataProtection()
        .PersistKeysToDbContext<AppDbContext>()
    .Services
    .AddAuthorizationBuilder()
        .AddPolicy("Admin", policy => policy.RequireClaim("IsAdmin", "True"))
    .Services
    .AddIdentityCore<User>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders()
    .Services
    .AddSingleton<IEmailSender, NoOpEmailSender>()
    .AddOptions()
    .AddHttpClient(
        "API",
        configureClient: client =>
        {
            client.BaseAddress = new Uri("http://localhost:8080");
        }
    )
    .Services
    .AddScoped<CategoryService>()
    .AddScoped<ProductService>()
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ProductCard).Assembly);

app.MapAdditionalIdentityEndpoints();

await app.RunAsync(cts.Token);
