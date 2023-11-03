using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Scroll.Data.Repositories;
using Scroll.Data.Repositories.EFCore;
using Scroll.Domain;
using Scroll.Domain.Entities;
using Minio;

namespace Scroll.Data;

public static class DataConfigurator
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("name=ConnectionStrings:ScrollDb"));

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        // services.AddScoped<IFileRepository, FileRepository>();

        services.AddSingleton<IMinioClientFactory, MinioClientFactory>(sp =>
        {
            var minioConfig = sp.GetRequiredService<IOptions<MinioConfig>>().Value;
            return
                new MinioClientFactory(config =>
                    config.WithEndpoint(minioConfig.Endpoint)
                        .WithCredentials(minioConfig.AccessKey, minioConfig.SecretKey)
                );
        });

        services.AddScoped(sp =>
        {
            var minioClientFactory = sp.GetRequiredService<IMinioClientFactory>();
            return minioClientFactory.CreateClient();
        });

        services.AddScoped<IFileRepository, MinioFileRepository>();

        return services;
    }

    public static async Task<IHost> EnsureDatabaseMigrated(this IHost host, CancellationToken token)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        if (await context.Database.GetPendingMigrationsAsync(token) is { } migrations
            && migrations.Any()
        )
        {
            await context.Database.MigrateAsync(token);
        }

        return host;
    }

    public static async Task<IHost> EnsureDatabaseSeeded(this IHost host, CancellationToken token)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var userStore = services.GetRequiredService<IUserStore<User>>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        foreach (var urm in FakeData.GenerateUsers(100))
        {
            var user = new User
            {
                FullName = urm.FullName
            };

            await userStore.SetUserNameAsync(user, urm.UserName, token);
            await userStore.SetNormalizedUserNameAsync(user, urm.UserName, token);
            await ((IUserEmailStore<User>)userStore).SetEmailAsync(user, urm.Email, token);
            await userManager.CreateAsync(user, urm.Password);
        }

        var productRepository = services.GetRequiredService<IRepository<ProductId, Product>>();
        if (await productRepository.Table.AnyAsync(token) is false)
        {
            var products = FakeData.GenerateProducts(100);
            await productRepository.Add(products, token);
        }

        return host;
    }
}