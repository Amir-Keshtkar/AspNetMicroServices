using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging((hostingContent, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContent.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddOcelot();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.UseOcelot();

app.Run();
