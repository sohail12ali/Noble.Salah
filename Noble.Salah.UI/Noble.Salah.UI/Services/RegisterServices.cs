using Noble.Salah.Common.Interfaces;
using Noble.Salah.Integration.Services;
using Noble.Salah.UI.Shared.Services;

namespace Noble.Salah.UI.Services;

internal static class RegisterServices
{
    public static IServiceCollection AddDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ThemeManager>()
           .AddSingleton<IFormFactor, FormFactor>()
           .AddSingleton<IPrayerService, PrayerService>()
           .AddSingleton<ILocalStorage, MauiLocalStorage>()
           .AddSingleton<ILocationService, MauiLocationService>();

        return services;
    }
}
