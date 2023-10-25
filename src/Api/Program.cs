using Scroll.Api;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

await WebApplication
    .CreateBuilder(args)
    .ConfigureAppBuilder()
    .Build()
    .ConfigurePipeline()
    .RunAsync(cts.Token);