using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Represents prayer tracking data for a specific date
/// </summary>
public record PrayerTrackingModel
{
    /// <summary>
    /// The date this tracking is for
    /// </summary>
    public DateTime Date { get; init; }
    
    /// <summary>
    /// Dictionary of prayer completion status
    /// </summary>
    public Dictionary<PrayerName, PrayerCompletionStatus> PrayerStatus { get; init; } = new();
    
    /// <summary>
    /// Total prayers completed for this date
    /// </summary>
    public int CompletedPrayers => PrayerStatus.Count(p => p.Value.IsCompleted);
    
    /// <summary>
    /// Total prayers for this date
    /// </summary>
    public int TotalPrayers => PrayerStatus.Count;
    
    /// <summary>
    /// Completion percentage for this date
    /// </summary>
    public double CompletionPercentage => TotalPrayers > 0 ? (double)CompletedPrayers / TotalPrayers * 100 : 0;
    
    /// <summary>
    /// Notes for this day
    /// </summary>
    public string? Notes { get; init; }
    
    /// <summary>
    /// Gets the completion status for a specific prayer
    /// </summary>
    public PrayerCompletionStatus GetPrayerStatus(PrayerName prayerName)
    {
        return PrayerStatus.TryGetValue(prayerName, out var status) ? status : new PrayerCompletionStatus();
    }
    
    /// <summary>
    /// Marks a prayer as completed
    /// </summary>
    public void MarkPrayerCompleted(PrayerName prayerName, DateTime? completedAt = null)
    {
        PrayerStatus[prayerName] = new PrayerCompletionStatus
        {
            IsCompleted = true,
            CompletedAt = completedAt ?? DateTime.Now,
            CompletedOnTime = IsPrayerCompletedOnTime(prayerName, completedAt ?? DateTime.Now)
        };
    }
    
    /// <summary>
    /// Checks if a prayer was completed on time
    /// </summary>
    private bool IsPrayerCompletedOnTime(PrayerName prayerName, DateTime completedAt)
    {
        // This would need to be implemented with actual prayer time windows
        // For now, we'll consider it on time if completed within 2 hours of prayer time
        return true; // Placeholder implementation
    }
}

/// <summary>
/// Represents the completion status of a prayer
/// </summary>
public record PrayerCompletionStatus
{
    /// <summary>
    /// Whether the prayer was completed
    /// </summary>
    public bool IsCompleted { get; init; }
    
    /// <summary>
    /// When the prayer was completed
    /// </summary>
    public DateTime? CompletedAt { get; init; }
    
    /// <summary>
    /// Whether the prayer was completed on time
    /// </summary>
    public bool CompletedOnTime { get; init; }
    
    /// <summary>
    /// Notes about the prayer completion
    /// </summary>
    public string? Notes { get; init; }
}
