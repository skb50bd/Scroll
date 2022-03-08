using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scroll.Service.Data;
using Scroll.Service.Services;

namespace Scroll.Service.DependencyInjection;

public static class ServiceConfigurator
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.ConfigureData(config);

        services.AddScoped<IPictureProcessor, PictureProcessor>();
        services.AddScoped<IPictureService, PictureService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}