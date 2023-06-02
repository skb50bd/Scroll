using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scroll.Core.Services;
using Scroll.Data;

namespace Scroll.Core;

public static class ServiceConfigurator
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.ConfigureData(config);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPictureProcessor, PictureProcessor>();
        services.AddScoped<IPictureService, PictureService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}