using Batoulapps.Adhan.Internal;
using Batoulapps.Adhan;
using Noble.Salah.Common.Interfaces;
using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;

namespace Noble.Salah.Integration.Services;

public class PrayerService : IPrayerService
{
    #region Fields
    private string _timeZoneId = string.Empty;
    private Coordinates _coordinates = new(0, 0);

    private CalculationMethod _method;
    private Madhab _madhab;
    #endregion

    #region Task & Methods

    public void UpdateConfigs(double latitude, double longitude, string timeZoneId, CalculationMethodBy method, SchoolOfThought madhab)
    {
        _timeZoneId = timeZoneId;

        UpdateCoordinates(latitude, longitude);
        UpdateSchoolOfThought(madhab);
        UpdateCalculationMethod(method);
    }

    private void UpdateCoordinates(double latitude, double longitude)
    {
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


    public PrayerTimesModel GetPrayerTimes(DateTime date)
    {
        var paramsConfig = CalculationMethodExtensions.GetParameters(_method);
        paramsConfig.Madhab = _madhab;

        var dateComponents = new DateComponents(date.Year, date.Month, date.Day);
        var prayerTimes = new PrayerTimes(_coordinates, dateComponents, paramsConfig);

        return new PrayerTimesModel(
            prayerTimes.Fajr,
            prayerTimes.Sunrise,
            prayerTimes.Dhuhr,
            prayerTimes.Asr,
            prayerTimes.Maghrib,
            prayerTimes.Isha
        );
    }

    public PrayerTimesModel GetTodayPrayerTimes()
    {
        return GetPrayerTimes(DateTime.Now);
    }

    public (PrayerName?, DateTime?) GetNextPrayer()
    {
        var times = GetTodayPrayerTimes();
        var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId));

        var prayerDict = new Dictionary<PrayerName, DateTime>
        {
            [PrayerName.Fajr] = times.Fajr,
            [PrayerName.Sunrise] = times.Sunrise,
            [PrayerName.Dhuhr] = times.Dhuhr,
            [PrayerName.Asr] = times.Asr,
            [PrayerName.Maghrib] = times.Maghrib,
            [PrayerName.Isha] = times.Isha
        };

        foreach (var pair in prayerDict)
        {
            if (pair.Value > now)
                return (pair.Key, pair.Value);
        }
        return (null, null);
    } 

    #endregion
}