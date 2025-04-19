using Microsoft.JSInterop;
using Noble.Salah.Common.Interfaces;
using Noble.Salah.Common.Models;

namespace Noble.Salah.UI.Web.Services;

public class BrowserLocationService(IJSRuntime js) : ILocationService
{
    public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
    {
        try
        {
            var position = await js.InvokeAsync<Geolocation>(
                "navigator.geolocation.getCurrentPositionAsync");
            return (position.Latitude, position.Longitude);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string> GetLocalTimeZoneIdAsync()
    {
        return await js.InvokeAsync<string>("getTimeZoneId");
    }
}
