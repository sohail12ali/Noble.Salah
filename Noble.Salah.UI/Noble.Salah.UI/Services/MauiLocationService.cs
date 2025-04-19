using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Services;

public class MauiLocationService : ILocationService
{
    public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
    {
        var location = await Geolocation.GetLastKnownLocationAsync()
                       ?? await Geolocation.GetLocationAsync(new GeolocationRequest
                       {
                           DesiredAccuracy = GeolocationAccuracy.Medium,
                           Timeout = TimeSpan.FromSeconds(10)
                       });

        if (location is null)
            return null;

        return (location.Latitude, location.Longitude);
    }

    public Task<string> GetLocalTimeZoneIdAsync()
       => Task.FromResult(TimeZoneInfo.Local.Id);
}
