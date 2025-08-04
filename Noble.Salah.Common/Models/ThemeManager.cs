using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.Common.Models;

/// <summary>
/// Manages application theme settings and persistence
/// </summary>
public class ThemeManager
{
    /// <summary>
    /// Current theme setting
    /// </summary>
    public AppTheme CurrentTheme { get; private set; } = AppTheme.System;
    
    /// <summary>
    /// Whether dark mode is currently active
    /// </summary>
    public bool IsDarkMode { get; private set; }
    
    /// <summary>
    /// Event raised when theme changes
    /// </summary>
    public event Action? ThemeChanged;
    
    /// <summary>
    /// Sets the current theme and persists it to storage
    /// </summary>
    /// <param name="theme">The theme to set</param>
    /// <param name="localStorage">Local storage service for persistence</param>
    public async Task SetThemeAsync(AppTheme theme, ILocalStorage localStorage)
    {
        CurrentTheme = theme;
        await localStorage.SaveAsync("AppTheme", theme.ToString());
        ThemeChanged?.Invoke();
    }
    
    /// <summary>
    /// Loads the saved theme from storage
    /// </summary>
    /// <param name="localStorage">Local storage service</param>
    public async Task LoadThemeAsync(ILocalStorage localStorage)
    {
        var savedTheme = await localStorage.LoadAsync<string>("AppTheme");
        if (!string.IsNullOrEmpty(savedTheme) && Enum.TryParse<AppTheme>(savedTheme, out var theme))
        {
            CurrentTheme = theme;
        }
    }
    
    /// <summary>
    /// Sets the dark mode state
    /// </summary>
    /// <param name="isDarkMode">Whether dark mode should be active</param>
    public void SetDarkMode(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
        ThemeChanged?.Invoke();
    }
    
    /// <summary>
    /// Gets the appropriate theme based on current settings and system preference
    /// </summary>
    /// <param name="systemPrefersDark">Whether the system prefers dark mode</param>
    /// <returns>The effective theme to use</returns>
    public AppTheme GetEffectiveTheme(bool systemPrefersDark)
    {
        return CurrentTheme switch
        {
            AppTheme.Light => AppTheme.Light,
            AppTheme.Dark => AppTheme.Dark,
            AppTheme.System => systemPrefersDark ? AppTheme.Dark : AppTheme.Light,
            _ => AppTheme.System
        };
    }
} 