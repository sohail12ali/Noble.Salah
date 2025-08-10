using Noble.Salah.Common.Interfaces;
using Noble.Salah.Integration.Services;
using Noble.Salah.UI.Shared.Services;

namespace Noble.Salah.UI.Web.Services;

internal static class RegisterServices
{
    public static IServiceCollection AddDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ThemeManager>()
            .AddSingleton<IFormFactor, FormFactor>()
            .AddSingleton<IPrayerService, PrayerService>()
            .AddSingleton<ILocalStorage, BrowserLocalStorage>()
            .AddSingleton<ILocationService, BrowserLocationService>();

        return services;
    }
}
