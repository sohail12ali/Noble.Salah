using Xunit;
using Noble.Salah.Common.Services;

namespace Noble.Salah.Tests;

/// <summary>
/// Unit tests for ValidationService
/// </summary>
public class ValidationServiceTests
{
    [Theory]
    [InlineData(0, true)]
    [InlineData(90, true)]
    [InlineData(-90, true)]
    [InlineData(45.5, true)]
    [InlineData(91, false)]
    [InlineData(-91, false)]
    [InlineData(180, false)]
    [InlineData(-180, false)]
    public void IsValidLatitude_WithVariousValues_ReturnsExpectedResult(double latitude, bool expected)
    {
        // Act
        var result = ValidationService.IsValidLatitude(latitude);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(180, true)]
    [InlineData(-180, true)]
    [InlineData(45.5, true)]
    [InlineData(181, false)]
    [InlineData(-181, false)]
    [InlineData(360, false)]
    [InlineData(-360, false)]
    public void IsValidLongitude_WithVariousValues_ReturnsExpectedResult(double longitude, bool expected)
    {
        // Act
        var result = ValidationService.IsValidLongitude(longitude);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("America/New_York", true)]
    [InlineData("Europe/London", true)]
    [InlineData("Asia/Tokyo", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("Invalid/Timezone", false)]
    [InlineData("NotARealTimezone", false)]
    public void IsValidTimezone_WithVariousValues_ReturnsExpectedResult(string timezoneId, bool expected)
    {
        // Act
        var result = ValidationService.IsValidTimezone(timezoneId);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2024, true)]
    [InlineData(2000, true)]
    [InlineData(2100, true)]
    [InlineData(1899, false)]
    [InlineData(2101, false)]
    [InlineData(1800, false)]
    [InlineData(2200, false)]
    public void IsValidPrayerDate_WithVariousYears_ReturnsExpectedResult(int year, bool expected)
    {
        // Arrange
        var date = new DateTime(year, 1, 1);
        
        // Act
        var result = ValidationService.IsValidPrayerDate(date);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123.45", true, 123.45)]
    [InlineData("-67.89", true, -67.89)]
    [InlineData("0", true, 0)]
    [InlineData("abc", false, 0)]
    [InlineData("", false, 0)]
    [InlineData(null, false, 0)]
    [InlineData("12.34.56", false, 0)]
    public void IsValidNumber_WithVariousValues_ReturnsExpectedResult(string value, bool expected, double expectedNumber)
    {
        // Act
        var result = ValidationService.IsValidNumber(value, out var number);
        
        // Assert
        Assert.Equal(expected, result);
        if (expected)
        {
            Assert.Equal(expectedNumber, number, 2);
        }
    }

    [Theory]
    [InlineData("Hello", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(null, false)]
    [InlineData("Valid String", true)]
    public void IsValidString_WithVariousValues_ReturnsExpectedResult(string value, bool expected)
    {
        // Act
        var result = ValidationService.IsValidString(value);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("en", true)]
    [InlineData("ar", true)]
    [InlineData("ur", true)]
    [InlineData("hi", true)]
    [InlineData("EN", true)] // Case insensitive
    [InlineData("AR", true)]
    [InlineData("fr", false)]
    [InlineData("es", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsSupportedLanguage_WithVariousValues_ReturnsExpectedResult(string languageCode, bool expected)
    {
        // Act
        var result = ValidationService.IsSupportedLanguage(languageCode);
        
        // Assert
        Assert.Equal(expected, result);
    }
} 