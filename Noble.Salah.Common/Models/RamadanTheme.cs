using MudBlazor;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Provides Ramadan-specific theme colors and styling
/// </summary>
public static class RamadanTheme
{
    /// <summary>
    /// Gets the Ramadan theme with purple and orange color scheme
    /// </summary>
    /// <returns>MudTheme configured for Ramadan</returns>
    public static MudTheme GetRamadanTheme()
    {
        // For now, return a basic dark theme
        // TODO: Implement proper Ramadan theme when MudBlazor structure is confirmed
        return new MudTheme();
    }
    
    /// <summary>
    /// Gets CSS variables for Ramadan theme
    /// </summary>
    /// <returns>Dictionary of CSS variable names and values</returns>
    public static Dictionary<string, string> GetRamadanCssVariables()
    {
        return new Dictionary<string, string>
        {
            ["--ramadan-primary"] = "#6B46C1",
            ["--ramadan-secondary"] = "#F59E0B",
            ["--ramadan-background"] = "#1F2937",
            ["--ramadan-surface"] = "#374151",
            ["--ramadan-text-primary"] = "#F9FAFB",
            ["--ramadan-text-secondary"] = "#D1D5DB",
            ["--ramadan-accent"] = "#8B5CF6",
            ["--ramadan-highlight"] = "#FBBF24"
        };
    }
    
    /// <summary>
    /// Gets Ramadan-specific prayer card styling
    /// </summary>
    /// <param name="isCurrentPrayer">Whether this is the current prayer</param>
    /// <returns>CSS style string for prayer cards</returns>
    public static string GetPrayerCardStyle(bool isCurrentPrayer = false)
    {
        var baseStyle = "background: linear-gradient(135deg, #6B46C1 0%, #8B5CF6 100%); border-radius: 12px; box-shadow: 0 4px 6px -1px rgba(107, 70, 193, 0.1);";
        
        if (isCurrentPrayer)
        {
            return baseStyle + " border: 3px solid #F59E0B; box-shadow: 0 0 20px rgba(245, 158, 11, 0.4);";
        }
        
        return baseStyle;
    }
    
    /// <summary>
    /// Gets Ramadan-specific text styling
    /// </summary>
    /// <returns>CSS style string for text elements</returns>
    public static string GetTextStyle()
    {
        return "color: #F9FAFB; text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);";
    }
} 