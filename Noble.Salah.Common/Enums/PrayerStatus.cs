namespace Noble.Salah.Common.Enums;

/// <summary>
/// Represents the status of a prayer
/// </summary>
public enum PrayerStatus
{
    /// <summary>
    /// Prayer is pending (not yet time)
    /// </summary>
    Pending,
    
    /// <summary>
    /// Prayer is currently active (within prayer time window)
    /// </summary>
    Current,
    
    /// <summary>
    /// Prayer is the next upcoming prayer
    /// </summary>
    Upcoming,
    
    /// <summary>
    /// Prayer has been completed
    /// </summary>
    Completed,
    
    /// <summary>
    /// Prayer time has passed without completion
    /// </summary>
    Missed
}
