using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Models;

namespace Noble.Salah.Common.Interfaces;

public interface IPrayerService
{
    #region Methods

    PrayerTimesModel GetPrayerTimes(DateTime date);
    PrayerTimesModel GetTodayPrayerTimes();
    (PrayerName? Name, DateTime? Time) GetNextPrayer();
    void UpdateConfigs(double latitude, double longitude, string timeZoneId, CalculationMethodBy method = CalculationMethodBy.MUSLIM_WORLD_LEAGUE, SchoolOfThought madhab = SchoolOfThought.SHAFI);

    #endregion
}
