using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scroll.Data.Repositories;

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
}