namespace Noble.Salah.Common.Interfaces;

public interface ILocationService
{
    Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync();

    Task<string> GetLocalTimeZoneIdAsync();
}
