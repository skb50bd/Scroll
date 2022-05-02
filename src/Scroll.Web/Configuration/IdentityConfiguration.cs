using Microsoft.AspNetCore.Identity;

namespace Scroll.Web.Configuration;

public static class IdentityConfiguration
{
    public static IServiceCollection ConfigureIdentity(
        this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit           = true;
            options.Password.RequireLowercase       = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase       = true;
            options.Password.RequiredLength         = 6;
            options.Password.RequiredUniqueChars    = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers      = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
            
            options.User.RequireUniqueEmail = true;
        });
        
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly   = true;
            options.ExpireTimeSpan    = TimeSpan.FromDays(7);
            options.LoginPath         = "/Login";
            options.AccessDeniedPath  = "/AccessDenied";
            options.SlidingExpiration = true;
        });

        return services;
    }
}