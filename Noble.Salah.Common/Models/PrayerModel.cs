using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Represents a prayer with its timing and metadata
/// </summary>
public record PrayerModel(
    PrayerName PrayerName, 
    DateTime PrayerTime, 
    string BackgroundImageSource,
    DateTime? CompletedAt = null,
    string? Notes = null)
{
    /// <summary>
    /// Gets or sets whether this prayer is completed
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Gets the formatted prayer time as a string
    /// </summary>
    public string FormattedTime => PrayerTime.ToString("HH:mm");
    
    /// <summary>
    /// Gets the prayer time in 12-hour format
    /// </summary>
    public string FormattedTime12Hour => PrayerTime.ToString("hh:mm tt");
    
    /// <summary>
    /// Gets or sets whether this prayer is the next upcoming prayer
    /// </summary>
    public bool IsNextPrayer { get; set; }
    
    /// <summary>
    /// Gets or sets whether this prayer is the current prayer
    /// </summary>
    public bool IsCurrentPrayer { get; set; }
    
    /// <summary>
    /// Gets or sets the time remaining until this prayer (if it's upcoming)
    /// </summary>
    public TimeSpan? TimeRemaining { get; set; }
    
    /// <summary>
    /// Gets the prayer status
    /// </summary>
    public PrayerStatus Status => IsCompleted ? PrayerStatus.Completed : 
                                 IsCurrentPrayer ? PrayerStatus.Current :
                                 IsNextPrayer ? PrayerStatus.Upcoming : 
                                 PrayerStatus.Pending;
}