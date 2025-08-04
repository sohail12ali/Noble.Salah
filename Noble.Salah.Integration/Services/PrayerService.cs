using Batoulapps.Adhan.Internal;
using Batoulapps.Adhan;
using Noble.Salah.Common.Interfaces;
using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Constants;

namespace Noble.Salah.Integration.Services;

public class PrayerService : IPrayerService
{
    #region Fields

    private string _timeZoneId = string.Empty;
    private Coordinates _coordinates = new(0, 0);

    private CalculationMethod _method;
    private Madhab _madhab;

    #endregion Fields

    #region Task & Methods

    #region Configs

    public void UpdateConfigs(double latitude, double longitude, string timeZoneId, CalculationMethodBy method = CalculationMethodBy.MUSLIM_WORLD_LEAGUE, SchoolOfThought madhab = SchoolOfThought.SHAFI)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(timeZoneId))
            {
                throw new ArgumentException("Timezone ID cannot be null or empty.", nameof(timeZoneId));
            }

            // Validate timezone exists
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException($"Timezone '{timeZoneId}' not found.", nameof(timeZoneId));
            }

            _timeZoneId = timeZoneId;
            UpdateCoordinates(latitude, longitude);
            UpdateSchoolOfThought(madhab);
            UpdateCalculationMethod(method);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating prayer service configs: {ex.Message}");
            throw; // Re-throw to allow calling code to handle
        }
    }

    private void UpdateCoordinates(double latitude, double longitude)
    {
        // Validate latitude and longitude
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");
        }
        
        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");
        }
        
        _coordinates = new Coordinates(latitude, longitude);
    }

    private void UpdateCalculationMethod(CalculationMethodBy method)
    {
        _method = method switch
        {
            CalculationMethodBy.MUSLIM_WORLD_LEAGUE => CalculationMethod.MUSLIM_WORLD_LEAGUE,
            CalculationMethodBy.EGYPTIAN => CalculationMethod.EGYPTIAN,
            CalculationMethodBy.KARACHI => CalculationMethod.KARACHI,
            CalculationMethodBy.UMM_AL_QURA => CalculationMethod.UMM_AL_QURA,
            CalculationMethodBy.DUBAI => CalculationMethod.DUBAI,
            CalculationMethodBy.MOON_SIGHTING_COMMITTEE => CalculationMethod.MOON_SIGHTING_COMMITTEE,
            CalculationMethodBy.NORTH_AMERICA => CalculationMethod.NORTH_AMERICA,
            CalculationMethodBy.KUWAIT => CalculationMethod.KUWAIT,
            CalculationMethodBy.QATAR => CalculationMethod.QATAR,
            CalculationMethodBy.SINGAPORE => CalculationMethod.SINGAPORE,
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    private void UpdateSchoolOfThought(SchoolOfThought madhab)
    {
        _madhab = madhab switch
        {
            SchoolOfThought.SHAFI => Madhab.SHAFI,
            SchoolOfThought.HANAFI => Madhab.HANAFI,
            _ => throw new ArgumentOutOfRangeException(nameof(madhab), madhab, null)
        };
    }

    #endregion Configs

    public PrayerTimesModel GetPrayerTimes(DateTime date)
    {
        try
        {
            // Validate coordinates
            if (_coordinates.Latitude == 0 && _coordinates.Longitude == 0)
            {
                throw new InvalidOperationException("Location coordinates not set. Please update location settings.");
            }

            // Validate timezone
            if (string.IsNullOrEmpty(_timeZoneId))
            {
                throw new InvalidOperationException("Timezone not set. Please update location settings.");
            }

            var paramsConfig = CalculationMethodExtensions.GetParameters(_method);
            paramsConfig.Madhab = _madhab;

            var dateComponents = new DateComponents(date.Year, date.Month, date.Day);
            var prayerTimes = new PrayerTimes(_coordinates, dateComponents, paramsConfig);

            return new PrayerTimesModel(
                prayerTimes.Fajr.ToLocalTime(),
                prayerTimes.Sunrise.ToLocalTime(),
                prayerTimes.Dhuhr.ToLocalTime(),
                prayerTimes.Asr.ToLocalTime(),
                prayerTimes.Maghrib.ToLocalTime(),
                prayerTimes.Isha.ToLocalTime()
            );
        }
        catch (Exception ex)
        {
            // Log error for debugging
            Console.WriteLine($"Error calculating prayer times: {ex.Message}");
            
            // Return default prayer times for the current time
            return GetDefaultPrayerTimes(date);
        }
    }

    /// <summary>
    /// Returns default prayer times when calculation fails
    /// </summary>
    private PrayerTimesModel GetDefaultPrayerTimes(DateTime date)
    {
        var baseTime = date.Date.AddHours(6); // Start at 6 AM
        return new PrayerTimesModel(
            baseTime.AddHours(-2),      // Fajr at 4 AM
            baseTime.AddHours(-1),      // Sunrise at 5 AM
            baseTime.AddHours(6),       // Dhuhr at 12 PM
            baseTime.AddHours(9),       // Asr at 3 PM
            baseTime.AddHours(12),      // Maghrib at 6 PM
            baseTime.AddHours(13)       // Isha at 7 PM
        );
    }

    public PrayerTimesModel GetTodayPrayerTimes()
    {
        return GetPrayerTimes(DateTime.Now);
    }

    public (PrayerName?, DateTime?) GetNextPrayer()
    {
        var times = GetTodayPrayerTimes();
        
        DateTime now;
        try
        {
            if (string.IsNullOrEmpty(_timeZoneId))
            {
                now = DateTime.Now;
            }
            else
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId);
                now = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
            }
        }
        catch (TimeZoneNotFoundException)
        {
            // Fallback to local time if timezone is not found
            now = DateTime.Now;
        }

        var prayerDict = new Dictionary<PrayerName, DateTime>
        {
            [PrayerName.Fajr] = times.Fajr.ToLocalTime(),
            [PrayerName.Sunrise] = times.Sunrise.ToLocalTime(),
            [PrayerName.Dhuhr] = times.Dhuhr.ToLocalTime(),
            [PrayerName.Asr] = times.Asr.ToLocalTime(),
            [PrayerName.Maghrib] = times.Maghrib.ToLocalTime(),
            [PrayerName.Isha] = times.Isha.ToLocalTime()
        };

        foreach (var pair in prayerDict)
        {
            if (pair.Value > now)
                return (pair.Key, pair.Value);
        }
        return (null, null);
    }

    public IList<PrayerModel> GetPrayers(DateTime date)
    {
        var prayerTimes = GetPrayerTimes(date);
        var prayerList = new List<PrayerModel>
        {
            new(PrayerName.Fajr, prayerTimes.Fajr.ToLocalTime(), AppConstants.Images.Fajr),
            new(PrayerName.Sunrise, prayerTimes.Sunrise.ToLocalTime(), AppConstants.Images.Sunrise),
            new(PrayerName.Dhuhr, prayerTimes.Dhuhr.ToLocalTime(), AppConstants.Images.Dhuhr),
            new(PrayerName.Asr, prayerTimes.Asr.ToLocalTime(), AppConstants.Images.Asr),
            new(PrayerName.Maghrib, prayerTimes.Maghrib.ToLocalTime(), AppConstants.Images.Maghrib),
            new(PrayerName.Isha, prayerTimes.Isha.ToLocalTime(), AppConstants.Images.Isha)
        };
        return prayerList;
    }

    public IList<PrayerModel> GetTodayPrayers()
    {
        return GetPrayers(DateTime.Now);
    }

    #endregion Task & Methods
}