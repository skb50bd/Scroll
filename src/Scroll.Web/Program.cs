using Scroll.Library.Models.Mappers;
using Scroll.Service.DependencyInjection;
using Scroll.Web;
using Scroll.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(
    typeof(MappingProfile).Assembly);

builder.Services.AddControllers();

var mvcBuilder = builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services.AddTransient<PictureUploadService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddOptions();
builder.Services.Configure<SiteSetting>(
    builder.Configuration.GetSection(SiteSetting.Key));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();