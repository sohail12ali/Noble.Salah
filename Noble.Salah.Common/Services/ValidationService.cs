using System.Globalization;

namespace Noble.Salah.Common.Services;

/// <summary>
/// Provides validation utilities for the application
/// </summary>
public static class ValidationService
{
    /// <summary>
    /// Validates if a latitude value is within valid range
    /// </summary>
    /// <param name="latitude">Latitude value to validate</param>
    /// <returns>True if latitude is valid, false otherwise</returns>
    public static bool IsValidLatitude(double latitude)
    {
        return latitude >= -90 && latitude <= 90;
    }
    
    /// <summary>
    /// Validates if a longitude value is within valid range
    /// </summary>
    /// <param name="longitude">Longitude value to validate</param>
    /// <returns>True if longitude is valid, false otherwise</returns>
    public static bool IsValidLongitude(double longitude)
    {
        return longitude >= -180 && longitude <= 180;
    }
    
    /// <summary>
    /// Validates if a timezone ID is valid
    /// </summary>
    /// <param name="timezoneId">Timezone ID to validate</param>
    /// <returns>True if timezone ID is valid, false otherwise</returns>
    public static bool IsValidTimezone(string timezoneId)
    {
        if (string.IsNullOrWhiteSpace(timezoneId))
            return false;
            
        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return true;
        }
        catch (TimeZoneNotFoundException)
        {
            return false;
        }
    }
    
    /// <summary>
    /// Validates if a date is reasonable for prayer calculations
    /// </summary>
    /// <param name="date">Date to validate</param>
    /// <returns>True if date is reasonable, false otherwise</returns>
    public static bool IsValidPrayerDate(DateTime date)
    {
        // Prayer calculations are valid for dates between 1900 and 2100
        return date.Year >= 1900 && date.Year <= 2100;
    }
    
    /// <summary>
    /// Validates if a string is a valid number
    /// </summary>
    /// <param name="value">String value to validate</param>
    /// <param name="number">Output number if valid</param>
    /// <returns>True if string is a valid number, false otherwise</returns>
    public static bool IsValidNumber(string value, out double number)
    {
        return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out number);
    }
    
    /// <summary>
    /// Validates if a string is not null, empty, or whitespace
    /// </summary>
    /// <param name="value">String to validate</param>
    /// <returns>True if string is valid, false otherwise</returns>
    public static bool IsValidString(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Validates if a language code is supported
    /// </summary>
    /// <param name="languageCode">Language code to validate</param>
    /// <returns>True if language is supported, false otherwise</returns>
    public static bool IsSupportedLanguage(string languageCode)
    {
        var supportedLanguages = new[] { "en", "ar", "ur", "hi" };
        return supportedLanguages.Contains(languageCode?.ToLowerInvariant());
    }
} 