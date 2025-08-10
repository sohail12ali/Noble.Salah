using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudExtensions.Services;
using Noble.Salah.Common.Interfaces;
using Noble.Salah.UI.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add error handling and logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.RootComponents.Add<Noble.Salah.UI.Shared.Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services with error handling
try
{
    builder.Services.AddMudServices();
    builder.Services.AddMudExtensions();
    builder.Services.AddDependencyServices();
    builder.Services.AddSingleton<IFormFactor, FormFactor>();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
}
catch (Exception ex)
{
    Console.WriteLine($"Error configuring services: {ex.Message}");
    throw;
}

var host = builder.Build();

// Initialize services with error handling
try
{
    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting application: {ex.Message}");
    throw;
}
