using Noble.Salah.Common.Interfaces;
using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.Integration.Services;

/// <summary>
/// Service for managing Tasbeeh (Dhikr) counting
/// </summary>
public class TasbeehService : ITasbeehService
{
    private readonly ILocalStorage _localStorage;
    private const string CurrentTasbeehKey = "CurrentTasbeeh";
    private const string TasbeehHistoryKeyPrefix = "TasbeehHistory_";
    private const string TasbeehPresetsKey = "TasbeehPresets";
    private const string TasbeehStatisticsKey = "TasbeehStatistics";

    public TasbeehService(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    /// <summary>
    /// Creates a new Tasbeeh session
    /// </summary>
    public async Task<TasbeehModel> CreateTasbeehAsync(TasbeehType type, int target, string? customText = null)
    {
        var tasbeeh = new TasbeehModel
        {
            Type = type,
            Target = target,
            CustomText = customText
        };

        await _localStorage.SaveAsync(CurrentTasbeehKey, tasbeeh);
        await SaveTasbeehToHistoryAsync(tasbeeh);
        
        return tasbeeh;
    }

    /// <summary>
    /// Increments the Tasbeeh count
    /// </summary>
    public async Task<TasbeehModel> IncrementTasbeehAsync(Guid tasbeehId)
    {
        var tasbeeh = await GetCurrentTasbeehAsync();
        if (tasbeeh == null || tasbeeh.Id != tasbeehId)
        {
            throw new InvalidOperationException("Tasbeeh session not found");
        }

        tasbeeh.Count++;
        tasbeeh.LastUpdatedAt = DateTime.Now;

        await _localStorage.SaveAsync(CurrentTasbeehKey, tasbeeh);
        await SaveTasbeehToHistoryAsync(tasbeeh);

        return tasbeeh;
    }

    /// <summary>
    /// Decrements the Tasbeeh count
    /// </summary>
    public async Task<TasbeehModel> DecrementTasbeehAsync(Guid tasbeehId)
    {
        var tasbeeh = await GetCurrentTasbeehAsync();
        if (tasbeeh == null || tasbeeh.Id != tasbeehId)
        {
            throw new InvalidOperationException("Tasbeeh session not found");
        }

        if (tasbeeh.Count > 0)
        {
            tasbeeh.Count--;
            tasbeeh.LastUpdatedAt = DateTime.Now;

            await _localStorage.SaveAsync(CurrentTasbeehKey, tasbeeh);
            await SaveTasbeehToHistoryAsync(tasbeeh);
        }

        return tasbeeh;
    }

    /// <summary>
    /// Resets the Tasbeeh count
    /// </summary>
    public async Task<TasbeehModel> ResetTasbeehAsync(Guid tasbeehId)
    {
        var tasbeeh = await GetCurrentTasbeehAsync();
        if (tasbeeh == null || tasbeeh.Id != tasbeehId)
        {
            throw new InvalidOperationException("Tasbeeh session not found");
        }

        tasbeeh.Count = 0;
        tasbeeh.LastUpdatedAt = DateTime.Now;

        await _localStorage.SaveAsync(CurrentTasbeehKey, tasbeeh);
        await SaveTasbeehToHistoryAsync(tasbeeh);

        return tasbeeh;
    }

    /// <summary>
    /// Gets the current Tasbeeh session
    /// </summary>
    public async Task<TasbeehModel?> GetCurrentTasbeehAsync()
    {
        return await _localStorage.LoadAsync<TasbeehModel>(CurrentTasbeehKey);
    }

    /// <summary>
    /// Gets all Tasbeeh sessions for today
    /// </summary>
    public async Task<IList<TasbeehModel>> GetTodayTasbeehAsync()
    {
        var today = DateTime.Now.Date;
        var key = GetHistoryKey(today);
        return await _localStorage.LoadAsync<IList<TasbeehModel>>(key) ?? new List<TasbeehModel>();
    }

    /// <summary>
    /// Gets Tasbeeh sessions for a date range
    /// </summary>
    public async Task<IList<TasbeehModel>> GetTasbeehRangeAsync(DateTime startDate, DateTime endDate)
    {
        var tasbeehList = new List<TasbeehModel>();

        for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            var key = GetHistoryKey(date);
            var dayTasbeeh = await _localStorage.LoadAsync<IList<TasbeehModel>>(key);
            if (dayTasbeeh != null)
            {
                tasbeehList.AddRange(dayTasbeeh);
            }
        }

        return tasbeehList;
    }

    /// <summary>
    /// Gets Tasbeeh statistics
    /// </summary>
    public async Task<TasbeehStatistics> GetTasbeehStatisticsAsync()
    {
        var today = DateTime.Now.Date;
        var weekStart = today.AddDays(-(int)today.DayOfWeek);
        var monthStart = new DateTime(today.Year, today.Month, 1);

        var todayTasbeeh = await GetTodayTasbeehAsync();
        var weekTasbeeh = await GetTasbeehRangeAsync(weekStart, today);
        var monthTasbeeh = await GetTasbeehRangeAsync(monthStart, today);
        var allTasbeeh = await GetTasbeehRangeAsync(DateTime.MinValue, today);

        var todayCompleted = todayTasbeeh.Count(t => t.IsCompleted);
        var weekCompleted = weekTasbeeh.Count(t => t.IsCompleted);
        var monthCompleted = monthTasbeeh.Count(t => t.IsCompleted);
        var totalCompleted = allTasbeeh.Count(t => t.IsCompleted);

        // Calculate most used type
        var typeCounts = allTasbeeh.GroupBy(t => t.Type)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        var mostUsedType = typeCounts?.Type;

        // Calculate streaks (simplified implementation)
        var currentStreak = await CalculateCurrentStreakAsync();
        var longestStreak = await GetLongestStreakAsync();

        return new TasbeehStatistics
        {
            TodayCompleted = todayCompleted,
            WeekCompleted = weekCompleted,
            MonthCompleted = monthCompleted,
            TotalCompleted = totalCompleted,
            MostUsedType = mostUsedType,
            CurrentStreak = currentStreak,
            LongestStreak = longestStreak
        };
    }

    /// <summary>
    /// Gets predefined Tasbeeh presets
    /// </summary>
    public async Task<IList<TasbeehPreset>> GetTasbeehPresetsAsync()
    {
        var customPresets = await _localStorage.LoadAsync<IList<TasbeehPreset>>(TasbeehPresetsKey) ?? new List<TasbeehPreset>();

        var defaultPresets = new List<TasbeehPreset>
        {
            new() { Name = "SubhanAllah 33", Type = TasbeehType.SubhanAllah, Target = 33, Description = "سبحان الله 33 times", IsDefault = true },
            new() { Name = "Alhamdulillah 33", Type = TasbeehType.Alhamdulillah, Target = 33, Description = "الحمد لله 33 times", IsDefault = true },
            new() { Name = "AllahuAkbar 33", Type = TasbeehType.AllahuAkbar, Target = 33, Description = "الله أكبر 33 times", IsDefault = true },
            new() { Name = "LaIlahaIllallah 100", Type = TasbeehType.LaIlahaIllallah, Target = 100, Description = "لا إله إلا الله 100 times", IsDefault = true },
            new() { Name = "Morning Dhikr", Type = TasbeehType.SubhanAllah, Target = 99, Description = "Complete morning dhikr", IsDefault = true },
            new() { Name = "Evening Dhikr", Type = TasbeehType.SubhanAllah, Target = 99, Description = "Complete evening dhikr", IsDefault = true }
        };

        return defaultPresets.Concat(customPresets).ToList();
    }

    /// <summary>
    /// Saves a custom Tasbeeh preset
    /// </summary>
    public async Task SaveTasbeehPresetAsync(TasbeehPreset preset)
    {
        var existingPresets = await _localStorage.LoadAsync<IList<TasbeehPreset>>(TasbeehPresetsKey) ?? new List<TasbeehPreset>();
        
        // Remove existing preset with same name if it exists
        existingPresets = existingPresets.Where(p => p.Name != preset.Name).ToList();
        existingPresets.Add(preset);

        await _localStorage.SaveAsync(TasbeehPresetsKey, existingPresets);
    }

    /// <summary>
    /// Deletes a Tasbeeh session
    /// </summary>
    public async Task DeleteTasbeehAsync(Guid tasbeehId)
    {
        var currentTasbeeh = await GetCurrentTasbeehAsync();
        if (currentTasbeeh?.Id == tasbeehId)
        {
            await _localStorage.RemoveAsync(CurrentTasbeehKey);
        }

        // Also remove from history
        var today = DateTime.Now.Date;
        var todayTasbeeh = await GetTodayTasbeehAsync();
        var updatedTasbeeh = todayTasbeeh.Where(t => t.Id != tasbeehId).ToList();
        
        var key = GetHistoryKey(today);
        await _localStorage.SaveAsync(key, updatedTasbeeh);
    }

    /// <summary>
    /// Exports Tasbeeh data
    /// </summary>
    public async Task<byte[]> ExportTasbeehAsync(DateTime startDate, DateTime endDate, ExportFormat format = ExportFormat.CSV)
    {
        var tasbeehList = await GetTasbeehRangeAsync(startDate, endDate);

        return format switch
        {
            ExportFormat.CSV => ExportToCsv(tasbeehList),
            ExportFormat.JSON => ExportToJson(tasbeehList),
            ExportFormat.PDF => ExportToPdf(tasbeehList),
            _ => throw new ArgumentOutOfRangeException(nameof(format))
        };
    }

    /// <summary>
    /// Saves Tasbeeh to history
    /// </summary>
    private async Task SaveTasbeehToHistoryAsync(TasbeehModel tasbeeh)
    {
        var today = DateTime.Now.Date;
        var key = GetHistoryKey(today);
        var todayTasbeeh = await GetTodayTasbeehAsync();

        // Update existing or add new
        var existingIndex = todayTasbeeh.ToList().FindIndex(t => t.Id == tasbeeh.Id);
        if (existingIndex >= 0)
        {
            todayTasbeeh[existingIndex] = tasbeeh;
        }
        else
        {
            todayTasbeeh.Add(tasbeeh);
        }

        await _localStorage.SaveAsync(key, todayTasbeeh);
    }

    /// <summary>
    /// Gets the history key for a specific date
    /// </summary>
    private static string GetHistoryKey(DateTime date)
    {
        return $"{TasbeehHistoryKeyPrefix}{date:yyyy-MM-dd}";
    }

    /// <summary>
    /// Calculates current streak
    /// </summary>
    private async Task<int> CalculateCurrentStreakAsync()
    {
        var currentDate = DateTime.Now.Date;
        var streak = 0;

        while (true)
        {
            var dayTasbeeh = await GetTasbeehRangeAsync(currentDate, currentDate);
            if (!dayTasbeeh.Any(t => t.IsCompleted))
            {
                break;
            }

            streak++;
            currentDate = currentDate.AddDays(-1);
        }

        return streak;
    }

    /// <summary>
    /// Gets the longest streak
    /// </summary>
    private async Task<int> GetLongestStreakAsync()
    {
        var statistics = await _localStorage.LoadAsync<TasbeehStatistics>(TasbeehStatisticsKey);
        return statistics?.LongestStreak ?? 0;
    }

    /// <summary>
    /// Exports data to CSV format
    /// </summary>
    private static byte[] ExportToCsv(IList<TasbeehModel> tasbeehList)
    {
        var csv = new System.Text.StringBuilder();
        csv.AppendLine("Date,Type,Target,Count,Completed,StartedAt,LastUpdatedAt,Notes");
        
        foreach (var tasbeeh in tasbeehList)
        {
            csv.AppendLine($"{tasbeeh.StartedAt:yyyy-MM-dd},{tasbeeh.Type},{tasbeeh.Target},{tasbeeh.Count},{tasbeeh.IsCompleted},{tasbeeh.StartedAt:yyyy-MM-dd HH:mm},{tasbeeh.LastUpdatedAt:yyyy-MM-dd HH:mm},{tasbeeh.Notes ?? ""}");
        }
        
        return System.Text.Encoding.UTF8.GetBytes(csv.ToString());
    }

    /// <summary>
    /// Exports data to JSON format
    /// </summary>
    private static byte[] ExportToJson(IList<TasbeehModel> tasbeehList)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(tasbeehList, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Exports data to PDF format (placeholder implementation)
    /// </summary>
    private static byte[] ExportToPdf(IList<TasbeehModel> tasbeehList)
    {
        var text = $"Tasbeeh Report\nGenerated on: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n";
        
        foreach (var tasbeeh in tasbeehList)
        {
            text += $"Date: {tasbeeh.StartedAt:yyyy-MM-dd}\n";
            text += $"Type: {tasbeeh.Type}\n";
            text += $"Progress: {tasbeeh.Count}/{tasbeeh.Target} ({tasbeeh.ProgressPercentage:F1}%)\n";
            text += $"Completed: {(tasbeeh.IsCompleted ? "Yes" : "No")}\n";
            if (!string.IsNullOrEmpty(tasbeeh.Notes))
            {
                text += $"Notes: {tasbeeh.Notes}\n";
            }
            text += "\n";
        }
        
        return System.Text.Encoding.UTF8.GetBytes(text);
    }
}
