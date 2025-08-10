using Noble.Salah.Common.Interfaces;
using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;

namespace Noble.Salah.Integration.Services;

/// <summary>
/// Service for tracking prayer completion and statistics
/// </summary>
public class PrayerTrackingService : IPrayerTrackingService
{
    private readonly ILocalStorage _localStorage;
    private const string TrackingKeyPrefix = "PrayerTracking_";
    private const string StatisticsKey = "PrayerStatistics";

    public PrayerTrackingService(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    /// <summary>
    /// Marks a prayer as completed for a specific date
    /// </summary>
    public async Task MarkPrayerCompletedAsync(PrayerName prayerName, DateTime date, DateTime? completedAt = null, string? notes = null)
    {
        var tracking = await GetPrayerTrackingAsync(date) ?? new PrayerTrackingModel { Date = date };
        
        tracking.MarkPrayerCompleted(prayerName, completedAt);
        
        var key = GetTrackingKey(date);
        await _localStorage.SaveAsync(key, tracking);
        
        // Update statistics
        await UpdateStatisticsAsync();
    }

    /// <summary>
    /// Gets prayer tracking data for a specific date
    /// </summary>
    public async Task<PrayerTrackingModel?> GetPrayerTrackingAsync(DateTime date)
    {
        var key = GetTrackingKey(date);
        return await _localStorage.LoadAsync<PrayerTrackingModel>(key);
    }

    /// <summary>
    /// Gets prayer tracking data for a date range
    /// </summary>
    public async Task<IList<PrayerTrackingModel>> GetPrayerTrackingRangeAsync(DateTime startDate, DateTime endDate)
    {
        var trackingList = new List<PrayerTrackingModel>();
        
        for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            var tracking = await GetPrayerTrackingAsync(date);
            if (tracking != null)
            {
                trackingList.Add(tracking);
            }
        }
        
        return trackingList;
    }

    /// <summary>
    /// Gets prayer statistics for a date range
    /// </summary>
    public async Task<PrayerStatisticsModel> GetPrayerStatisticsAsync(DateTime startDate, DateTime endDate)
    {
        var trackingList = await GetPrayerTrackingRangeAsync(startDate, endDate);
        
        if (!trackingList.Any())
        {
            return new PrayerStatisticsModel();
        }
        
        var totalCompleted = trackingList.Sum(t => t.CompletedPrayers);
        var totalPrayers = trackingList.Sum(t => t.TotalPrayers);
        var averageCompletionPerDay = trackingList.Average(t => t.CompletionPercentage);
        
        // Calculate streaks
        var currentStreak = await GetCurrentStreakAsync();
        var longestStreak = await GetLongestStreakAsync();
        
        // Find most/least completed prayers
        var prayerCompletionCounts = new Dictionary<PrayerName, int>();
        foreach (var tracking in trackingList)
        {
            foreach (var prayerStatus in tracking.PrayerStatus)
            {
                if (prayerStatus.Value.IsCompleted)
                {
                    prayerCompletionCounts[prayerStatus.Key] = prayerCompletionCounts.GetValueOrDefault(prayerStatus.Key, 0) + 1;
                }
            }
        }
        
        var mostCompletedPrayer = prayerCompletionCounts.OrderByDescending(p => p.Value).FirstOrDefault().Key;
        var leastCompletedPrayer = prayerCompletionCounts.OrderBy(p => p.Value).FirstOrDefault().Key;
        
        return new PrayerStatisticsModel
        {
            TotalCompleted = totalCompleted,
            TotalPrayers = totalPrayers,
            CurrentStreak = currentStreak,
            LongestStreak = longestStreak,
            AverageCompletionPerDay = averageCompletionPerDay,
            MostCompletedPrayer = mostCompletedPrayer,
            LeastCompletedPrayer = leastCompletedPrayer
        };
    }

    /// <summary>
    /// Gets the current streak of completed prayers
    /// </summary>
    public async Task<int> GetCurrentStreakAsync()
    {
        var currentDate = DateTime.Now.Date;
        var streak = 0;
        
        while (true)
        {
            var tracking = await GetPrayerTrackingAsync(currentDate);
            if (tracking == null || tracking.CompletedPrayers == 0)
            {
                break;
            }
            
            streak++;
            currentDate = currentDate.AddDays(-1);
        }
        
        return streak;
    }

    /// <summary>
    /// Gets the longest streak of completed prayers
    /// </summary>
    public async Task<int> GetLongestStreakAsync()
    {
        var statistics = await _localStorage.LoadAsync<PrayerStatisticsModel>(StatisticsKey);
        return statistics?.LongestStreak ?? 0;
    }

    /// <summary>
    /// Exports prayer tracking data
    /// </summary>
    public async Task<byte[]> ExportPrayerTrackingAsync(DateTime startDate, DateTime endDate, ExportFormat format = ExportFormat.CSV)
    {
        var trackingList = await GetPrayerTrackingRangeAsync(startDate, endDate);
        
        return format switch
        {
            ExportFormat.CSV => ExportToCsv(trackingList),
            ExportFormat.JSON => ExportToJson(trackingList),
            ExportFormat.PDF => ExportToPdf(trackingList),
            _ => throw new ArgumentOutOfRangeException(nameof(format))
        };
    }

    /// <summary>
    /// Gets the storage key for a specific date
    /// </summary>
    private static string GetTrackingKey(DateTime date)
    {
        return $"{TrackingKeyPrefix}{date:yyyy-MM-dd}";
    }

    /// <summary>
    /// Updates the stored statistics
    /// </summary>
    private async Task UpdateStatisticsAsync()
    {
        var currentStreak = await GetCurrentStreakAsync();
        var existingStats = await _localStorage.LoadAsync<PrayerStatisticsModel>(StatisticsKey) ?? new PrayerStatisticsModel();
        
        var updatedStats = existingStats with
        {
            CurrentStreak = currentStreak,
            LongestStreak = Math.Max(existingStats.LongestStreak, currentStreak)
        };
        
        await _localStorage.SaveAsync(StatisticsKey, updatedStats);
    }

    /// <summary>
    /// Exports data to CSV format
    /// </summary>
    private static byte[] ExportToCsv(IList<PrayerTrackingModel> trackingList)
    {
        var csv = new System.Text.StringBuilder();
        csv.AppendLine("Date,CompletedPrayers,TotalPrayers,CompletionPercentage,Notes");
        
        foreach (var tracking in trackingList)
        {
            csv.AppendLine($"{tracking.Date:yyyy-MM-dd},{tracking.CompletedPrayers},{tracking.TotalPrayers},{tracking.CompletionPercentage:F1}%,{tracking.Notes ?? ""}");
        }
        
        return System.Text.Encoding.UTF8.GetBytes(csv.ToString());
    }

    /// <summary>
    /// Exports data to JSON format
    /// </summary>
    private static byte[] ExportToJson(IList<PrayerTrackingModel> trackingList)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(trackingList, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Exports data to PDF format (placeholder implementation)
    /// </summary>
    private static byte[] ExportToPdf(IList<PrayerTrackingModel> trackingList)
    {
        // This would require a PDF library like iText7 or PdfSharp
        // For now, return a simple text representation
        var text = $"Prayer Tracking Report\nGenerated on: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n";
        
        foreach (var tracking in trackingList)
        {
            text += $"Date: {tracking.Date:yyyy-MM-dd}\n";
            text += $"Completed: {tracking.CompletedPrayers}/{tracking.TotalPrayers} ({tracking.CompletionPercentage:F1}%)\n";
            if (!string.IsNullOrEmpty(tracking.Notes))
            {
                text += $"Notes: {tracking.Notes}\n";
            }
            text += "\n";
        }
        
        return System.Text.Encoding.UTF8.GetBytes(text);
    }
}
