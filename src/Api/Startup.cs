using System.Text.Json.Serialization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scroll.Api.Services;
using Scroll.Core;
using Scroll.Core.Services;
using Scroll.Data;
using Scroll.Domain.Entities;

namespace Scroll.Api;

public static class WebServicesConfiguration
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services
            .ConfigureServices()
            .AddScoped<IUserService, HttpUserService>()
            .AddScoped<PictureUploadService>()
            .AddScoped<FakeEmailSender>()
            .AddOptions()
                .Configure<SiteSetting>(configuration.GetSection(SiteSetting.Key))
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddHttpContextAccessor()
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            })
            .AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler       = ReferenceHandler.IgnoreCycles;
                o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }).Services
            .AddDataProtection()
                .PersistKeysToDbContext<AppDbContext>()
                .Services
            .AddAuthorizationBuilder()
                .AddPolicy(
                    "Admin",
                    policy => policy.RequireClaim("IsAdmin", "True")
                )
                .Services
            .AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<AppSignInManager>();

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
            .MapIdentityApi<User>()
            .WithDisplayName("Account")
            .WithDescription("Account management endpoints");

        app.MapControllers();

        return app;
    }
}