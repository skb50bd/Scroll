using Scroll.Web.Services;

namespace Scroll.Web.Configuration;

public static class WebServicesConfiguration
{
    public static IServiceCollection ConfigureWebAppServices(
        this IServiceCollection services)
    {
        services.AddTransient<PictureUploadService>();
        return services;
    }
}