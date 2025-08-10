using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Represents complete prayer times for a specific date
/// </summary>
public record PrayerTimesModel(
    DateTime Fajr, 
    DateTime Sunrise, 
    DateTime Dhuhr, 
    DateTime Asr, 
    DateTime Maghrib, 
    DateTime Isha)
{
    /// <summary>
    /// Gets the date these prayer times are for
    /// </summary>
    public DateTime Date => Fajr.Date;
    
    /// <summary>
    /// Gets the calculation method used
    /// </summary>
    public CalculationMethodBy CalculationMethod { get; set; }
    
    /// <summary>
    /// Gets the school of thought used
    /// </summary>
    public SchoolOfThought SchoolOfThought { get; set; }
    
    /// <summary>
    /// Gets the location coordinates used for calculation
    /// </summary>
    public (double Latitude, double Longitude) Coordinates { get; set; }
    
    /// <summary>
    /// Gets the timezone used for calculation
    /// </summary>
    public string TimeZoneId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets all prayer times as a dictionary
    /// </summary>
    public Dictionary<PrayerName, DateTime> AllPrayerTimes => new()
    {
        [PrayerName.Fajr] = Fajr,
        [PrayerName.Sunrise] = Sunrise,
        [PrayerName.Dhuhr] = Dhuhr,
        [PrayerName.Asr] = Asr,
        [PrayerName.Maghrib] = Maghrib,
        [PrayerName.Isha] = Isha
    };
    
    /// <summary>
    /// Gets the next prayer time from a given time
    /// </summary>
    public (PrayerName? Name, DateTime? Time) GetNextPrayer(DateTime fromTime)
    {
        var prayerTimes = AllPrayerTimes
            .Where(p => p.Value > fromTime)
            .OrderBy(p => p.Value)
            .FirstOrDefault();
            
        return (prayerTimes.Key, prayerTimes.Value);
    }
    
    /// <summary>
    /// Gets the current prayer time (if any) at a given time
    /// </summary>
    public (PrayerName? Name, DateTime? Time) GetCurrentPrayer(DateTime atTime)
    {
        // Define prayer time windows (typically 15-20 minutes)
        var prayerWindows = new Dictionary<PrayerName, (DateTime Start, DateTime End)>
        {
            [PrayerName.Fajr] = (Fajr, Fajr.AddMinutes(20)),
            [PrayerName.Sunrise] = (Sunrise, Sunrise.AddMinutes(10)),
            [PrayerName.Dhuhr] = (Dhuhr, Dhuhr.AddMinutes(20)),
            [PrayerName.Asr] = (Asr, Asr.AddMinutes(20)),
            [PrayerName.Maghrib] = (Maghrib, Maghrib.AddMinutes(20)),
            [PrayerName.Isha] = (Isha, Isha.AddMinutes(20))
        };
        
        var currentPrayer = prayerWindows
            .FirstOrDefault(p => atTime >= p.Value.Start && atTime <= p.Value.End);
            
        return (currentPrayer.Key, currentPrayer.Value.Start);
    }
}