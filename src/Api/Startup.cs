using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Scroll.Api.Services;
using Scroll.Core;
using Scroll.Core.Services;
using Scroll.Data;
using Scroll.Domain.Entities;

namespace Scroll.Api;

public static class WebServicesConfiguration
{
    public static WebApplicationBuilder ConfigureAppBuilder(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddHttpContextAccessor();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = ApiVersion.Default;
        });

        services.ConfigureServices();
        services.AddScoped<IUserService, HttpUserService>();
        services.AddScoped<PictureUploadService>();
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddOptions();
        services.Configure<SiteSetting>(configuration.GetSection(SiteSetting.Key));
        services.AddScoped<FakeEmailSender>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapGroup("/account")
            .MapIdentityApi<User>();

        app.MapControllers();

        return app;
    }
}