using Noble.Salah.Common.Interfaces;
using Noble.Salah.Integration.Services;

namespace Noble.Salah.UI.Web.Services;

internal static class RegisterServices
{
    public static IServiceCollection AddDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IFormFactor, FormFactor>()
            .AddSingleton<IPrayerService, PrayerService>()
            .AddSingleton<ILocationService, BrowserLocationService>();

        return services;
    }
}
