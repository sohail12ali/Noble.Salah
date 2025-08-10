using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Interfaces;

/// <summary>
/// Service for tracking prayer completion and statistics
/// </summary>
public interface IPrayerTrackingService
{
    /// <summary>
    /// Marks a prayer as completed for a specific date
    /// </summary>
    Task MarkPrayerCompletedAsync(PrayerName prayerName, DateTime date, DateTime? completedAt = null, string? notes = null);
    
    /// <summary>
    /// Gets prayer tracking data for a specific date
    /// </summary>
    Task<PrayerTrackingModel?> GetPrayerTrackingAsync(DateTime date);
    
    /// <summary>
    /// Gets prayer tracking data for a date range
    /// </summary>
    Task<IList<PrayerTrackingModel>> GetPrayerTrackingRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Gets prayer statistics for a date range
    /// </summary>
    Task<PrayerStatisticsModel> GetPrayerStatisticsAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Gets the current streak of completed prayers
    /// </summary>
    Task<int> GetCurrentStreakAsync();
    
    /// <summary>
    /// Gets the longest streak of completed prayers
    /// </summary>
    Task<int> GetLongestStreakAsync();
    
    /// <summary>
    /// Exports prayer tracking data
    /// </summary>
    Task<byte[]> ExportPrayerTrackingAsync(DateTime startDate, DateTime endDate, ExportFormat format = ExportFormat.CSV);
}

/// <summary>
/// Represents prayer statistics
/// </summary>
public record PrayerStatisticsModel
{
    /// <summary>
    /// Total prayers completed in the period
    /// </summary>
    public int TotalCompleted { get; init; }
    
    /// <summary>
    /// Total prayers in the period
    /// </summary>
    public int TotalPrayers { get; init; }
    
    /// <summary>
    /// Completion percentage
    /// </summary>
    public double CompletionPercentage => TotalPrayers > 0 ? (double)TotalCompleted / TotalPrayers * 100 : 0;
    
    /// <summary>
    /// Current streak
    /// </summary>
    public int CurrentStreak { get; init; }
    
    /// <summary>
    /// Longest streak
    /// </summary>
    public int LongestStreak { get; init; }
    
    /// <summary>
    /// Average completion per day
    /// </summary>
    public double AverageCompletionPerDay { get; init; }
    
    /// <summary>
    /// Most completed prayer
    /// </summary>
    public PrayerName? MostCompletedPrayer { get; init; }
    
    /// <summary>
    /// Least completed prayer
    /// </summary>
    public PrayerName? LeastCompletedPrayer { get; init; }
}

/// <summary>
/// Export format options
/// </summary>
public enum ExportFormat
{
    CSV,
    JSON,
    PDF
}
