using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scroll.Data.Repositories;
using Scroll.Data.Repositories.EFCore;
using Scroll.Domain.Entities;

namespace Scroll.Data;

public static class DataConfigurator
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("name=ConnectionStrings:ScrollDb"));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
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

        var productRepository = services.GetRequiredService<IRepository<Product>>();
        if (await productRepository.Table.AnyAsync(token) is false)
        {
            var products = FakeData.GenerateProducts(100);
            await productRepository.Add(products, token);
        }

        return host;
    }
}