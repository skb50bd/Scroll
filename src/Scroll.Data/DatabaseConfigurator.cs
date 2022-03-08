using Azure.Storage.Blobs;
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
            opt.UseSqlServer(
                config.GetConnectionString("ScrollDb")));

        services
            .AddIdentity<AppUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<ScrollDbContext>();

        // Add the Blob Storage Connection
        services.AddSingleton(x =>
            new BlobServiceClient(
                config.GetConnectionString("BlobStorage")));

        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IRepository<Product>, Repository<Product>>();
        services.AddScoped<IRepository<Category>, Repository<Category>>();

        return services;
    }
}