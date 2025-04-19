using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using MudExtensions.Services;
using Noble.Salah.Common.Interfaces;
using Noble.Salah.UI.Services;

namespace Noble.Salah.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Noble.Salah.UI.Shared project
            builder.Services.AddDependencyServices();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddMudExtensions();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
