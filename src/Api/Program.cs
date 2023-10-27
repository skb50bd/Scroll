using Scroll.Api;
using Scroll.Data;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().Build();
await app.EnsureDatabaseMigrated(cts.Token);

if (builder.Environment.IsDevelopment())
{
    await app.EnsureDatabaseSeeded(cts.Token);
}

await app.ConfigurePipeline().RunAsync(cts.Token);