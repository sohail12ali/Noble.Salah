using Microsoft.Extensions.DependencyInjection;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Shared.Services;

public static class RegisterServices
{
    public static IServiceCollection AddSharedDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ThemeManager>(sp => new ThemeManager(sp.GetRequiredService<ILocalStorage>()));
        return services;
    }
}