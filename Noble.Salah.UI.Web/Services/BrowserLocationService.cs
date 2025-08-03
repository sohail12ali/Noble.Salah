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
        try
        {
            var timeZoneId = await js.InvokeAsync<string>("getTimeZoneId");
            if (string.IsNullOrEmpty(timeZoneId))
            {
                // Fallback to a default timezone if JavaScript returns empty
                return "UTC";
            }
            return timeZoneId;
        }
        catch
        {
            // Fallback to UTC if JavaScript fails
            return "UTC";
        }
    }
}
