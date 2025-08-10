using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Represents a Tasbeeh counter session
/// </summary>
public record TasbeehModel
{
    /// <summary>
    /// Unique identifier for this Tasbeeh session
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// The type of Tasbeeh being counted
    /// </summary>
    public TasbeehType Type { get; init; }
    
    /// <summary>
    /// Current count
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Target count (e.g., 33, 99, 100)
    /// </summary>
    public int Target { get; init; }
    
    /// <summary>
    /// When this Tasbeeh session was started
    /// </summary>
    public DateTime StartedAt { get; init; } = DateTime.Now;
    
    /// <summary>
    /// When this Tasbeeh session was last updated
    /// </summary>
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Whether this session is completed
    /// </summary>
    public bool IsCompleted => Count >= Target;
    
    /// <summary>
    /// Progress percentage
    /// </summary>
    public double ProgressPercentage => Target > 0 ? (double)Count / Target * 100 : 0;
    
    /// <summary>
    /// Notes for this Tasbeeh session
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Custom Tasbeeh text (if not using predefined type)
    /// </summary>
    public string? CustomText { get; set; }
    
    /// <summary>
    /// Gets the display text for this Tasbeeh
    /// </summary>
    public string DisplayText => Type switch
    {
        TasbeehType.SubhanAllah => "سبحان الله",
        TasbeehType.Alhamdulillah => "الحمد لله",
        TasbeehType.AllahuAkbar => "الله أكبر",
        TasbeehType.LaIlahaIllallah => "لا إله إلا الله",
        TasbeehType.Custom => CustomText ?? "Custom",
        _ => "Unknown"
    };
    
    /// <summary>
    /// Gets the English translation
    /// </summary>
    public string EnglishTranslation => Type switch
    {
        TasbeehType.SubhanAllah => "Glory be to Allah",
        TasbeehType.Alhamdulillah => "Praise be to Allah",
        TasbeehType.AllahuAkbar => "Allah is the Greatest",
        TasbeehType.LaIlahaIllallah => "There is no god but Allah",
        TasbeehType.Custom => CustomText ?? "Custom",
        _ => "Unknown"
    };
}

/// <summary>
/// Represents a Tasbeeh preset
/// </summary>
public record TasbeehPreset
{
    /// <summary>
    /// Name of the preset
    /// </summary>
    public string Name { get; init; } = string.Empty;
    
    /// <summary>
    /// Type of Tasbeeh
    /// </summary>
    public TasbeehType Type { get; init; }
    
    /// <summary>
    /// Target count
    /// </summary>
    public int Target { get; init; }
    
    /// <summary>
    /// Description of the preset
    /// </summary>
    public string Description { get; init; } = string.Empty;
    
    /// <summary>
    /// Whether this is a default preset
    /// </summary>
    public bool IsDefault { get; init; }
}

/// <summary>
/// Represents Tasbeeh statistics
/// </summary>
public record TasbeehStatistics
{
    /// <summary>
    /// Total Tasbeeh completed today
    /// </summary>
    public int TodayCompleted { get; init; }
    
    /// <summary>
    /// Total Tasbeeh completed this week
    /// </summary>
    public int WeekCompleted { get; init; }
    
    /// <summary>
    /// Total Tasbeeh completed this month
    /// </summary>
    public int MonthCompleted { get; init; }
    
    /// <summary>
    /// Total Tasbeeh completed all time
    /// </summary>
    public int TotalCompleted { get; init; }
    
    /// <summary>
    /// Most used Tasbeeh type
    /// </summary>
    public TasbeehType? MostUsedType { get; init; }
    
    /// <summary>
    /// Current streak of daily Tasbeeh
    /// </summary>
    public int CurrentStreak { get; init; }
    
    /// <summary>
    /// Longest streak of daily Tasbeeh
    /// </summary>
    public int LongestStreak { get; init; }
}
