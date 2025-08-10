using Microsoft.Extensions.DependencyInjection;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Shared.Services;

public static class RegisterServices
{
    public static IServiceCollection AddSharedDependencyServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ThemeManager>(sp => 
            {
                var localStorage = sp.GetRequiredService<ILocalStorage>();
                var themeManager = new ThemeManager(localStorage);
                // Initialize theme loading synchronously to avoid navigation issues
                // The theme will be loaded when the component first renders
                return themeManager;
            });
        return services;
    }
}