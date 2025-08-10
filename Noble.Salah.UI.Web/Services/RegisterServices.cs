using Noble.Salah.Common.Interfaces;
using Noble.Salah.Integration.Services;
using Noble.Salah.UI.Shared.Services;

namespace Noble.Salah.UI.Web.Services;

internal static class RegisterServices
{
    public static IServiceCollection AddDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IFormFactor, FormFactor>()
            .AddSingleton<IPrayerService, PrayerService>()
            .AddSingleton<IPrayerTrackingService, PrayerTrackingService>()
            .AddSingleton<ITasbeehService, TasbeehService>()
            .AddSingleton<ILocalStorage, BrowserLocalStorage>()
            .AddSingleton<ILocationService, BrowserLocationService>()
            .AddSharedDependencyServices();

        return services;
    }
}