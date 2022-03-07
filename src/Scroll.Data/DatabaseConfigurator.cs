using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scroll.Data;

namespace Scroll.Service.Data;

public static class DatabaseConfigurator
{
    public static IServiceCollection ConfigureData(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ScrollDbContext>(opt =>
            opt.UseSqlServer(
                config.GetConnectionString("ScrollDb")));

        services.AddScoped<ImageRepository>();
        services.AddScoped<ProductRepository>();

        return services;
    }
}