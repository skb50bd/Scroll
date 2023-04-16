using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scroll.Data;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Data;

public static class DatabaseConfigurator
{
    public static IServiceCollection ConfigureData(
        this IServiceCollection services,
        IConfiguration config)
    {
        // Add the SQL Server DB Connection
        services.AddDbContext<ScrollDbContext>(opt =>
            opt.UseNpgsql(
                config.GetConnectionString("ScrollDb")));

        services
            .AddIdentity<AppUser, IdentityRole<int>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ScrollDbContext>();

        services.AddScoped<IRepository<ScrollFileInfo>, EntityRepository<ScrollFileInfo>>();
        services.AddScoped<IRepository<ScrollFile>, EntityRepository<ScrollFile>>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IEntityRepository<Product>, EntityRepository<Product>>();
        services.AddScoped<IEntityRepository<Category>, EntityRepository<Category>>();
        services.AddScoped<IRepository<ProductCategoryMapping>, Repository<ProductCategoryMapping>>();

        return services;
    }
}