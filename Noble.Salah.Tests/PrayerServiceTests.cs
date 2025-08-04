using Xunit;
using Noble.Salah.Integration.Services;
using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Models;

namespace Noble.Salah.Tests;

/// <summary>
/// Unit tests for PrayerService
/// </summary>
public class PrayerServiceTests
{
    private readonly PrayerService _prayerService;

    public PrayerServiceTests()
    {
        _prayerService = new PrayerService();
    }

    [Fact]
    public void GetPrayerTimes_WithValidCoordinates_ReturnsPrayerTimes()
    {
        // Arrange
        _prayerService.UpdateConfigs(40.7128, -74.0060, "America/New_York");
        
        // Act
        var prayerTimes = _prayerService.GetPrayerTimes(DateTime.Today);
        
        // Assert
        Assert.NotNull(prayerTimes);
        Assert.NotNull(prayerTimes.Fajr);
        Assert.NotNull(prayerTimes.Dhuhr);
        Assert.NotNull(prayerTimes.Asr);
        Assert.NotNull(prayerTimes.Maghrib);
        Assert.NotNull(prayerTimes.Isha);
        Assert.NotNull(prayerTimes.Sunrise);
    }

    [Fact]
    public void GetPrayerTimes_WithInvalidCoordinates_ReturnsDefaultPrayerTimes()
    {
        // Arrange - Don't set coordinates
        
        // Act
        var prayerTimes = _prayerService.GetPrayerTimes(DateTime.Today);
        
        // Assert
        Assert.NotNull(prayerTimes);
        // Should return default prayer times when coordinates are not set
        // Verify that prayer times are set for today's date
        Assert.Equal(DateTime.Today.Date, prayerTimes.Fajr.Date);
        Assert.Equal(DateTime.Today.Date, prayerTimes.Dhuhr.Date);
        Assert.Equal(DateTime.Today.Date, prayerTimes.Asr.Date);
        Assert.Equal(DateTime.Today.Date, prayerTimes.Maghrib.Date);
        Assert.Equal(DateTime.Today.Date, prayerTimes.Isha.Date);
        Assert.Equal(DateTime.Today.Date, prayerTimes.Sunrise.Date);
    }

    [Fact]
    public void UpdateConfigs_WithValidParameters_DoesNotThrow()
    {
        // Arrange
        var latitude = 40.7128;
        var longitude = -74.0060;
        var timezone = "America/New_York";
        
        // Act & Assert
        var exception = Record.Exception(() => 
            _prayerService.UpdateConfigs(latitude, longitude, timezone));
        
        Assert.Null(exception);
    }

    [Fact]
    public void UpdateConfigs_WithInvalidLatitude_ThrowsArgumentException()
    {
        // Arrange
        var invalidLatitude = 100.0; // Invalid latitude
        var longitude = -74.0060;
        var timezone = "America/New_York";
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            _prayerService.UpdateConfigs(invalidLatitude, longitude, timezone));
    }

    [Fact]
    public void UpdateConfigs_WithInvalidLongitude_ThrowsArgumentException()
    {
        // Arrange
        var latitude = 40.7128;
        var invalidLongitude = 200.0; // Invalid longitude
        var timezone = "America/New_York";
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            _prayerService.UpdateConfigs(latitude, invalidLongitude, timezone));
    }

    [Fact]
    public void UpdateConfigs_WithInvalidTimezone_ThrowsArgumentException()
    {
        // Arrange
        var latitude = 40.7128;
        var longitude = -74.0060;
        var invalidTimezone = "Invalid/Timezone";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _prayerService.UpdateConfigs(latitude, longitude, invalidTimezone));
    }

    [Fact]
    public void GetTodayPrayers_ReturnsCorrectNumberOfPrayers()
    {
        // Arrange
        _prayerService.UpdateConfigs(40.7128, -74.0060, "America/New_York");
        
        // Act
        var prayers = _prayerService.GetTodayPrayers();
        
        // Assert
        Assert.NotNull(prayers);
        Assert.Equal(6, prayers.Count); // 5 prayers + sunrise
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Fajr);
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Dhuhr);
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Asr);
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Maghrib);
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Isha);
        Assert.Contains(prayers, p => p.PrayerName == PrayerName.Sunrise);
    }

    [Fact]
    public void GetNextPrayer_WithValidData_ReturnsNextPrayer()
    {
        // Arrange
        _prayerService.UpdateConfigs(40.7128, -74.0060, "America/New_York");
        
        // Act
        var (nextPrayer, nextPrayerTime) = _prayerService.GetNextPrayer();
        
        // Assert
        // Note: This test might be flaky depending on the time of day
        // In a real scenario, you'd mock the current time
        if (nextPrayer.HasValue)
        {
            Assert.NotNull(nextPrayerTime);
            // The next prayer time should be valid (not null) and should be a reasonable time
            Assert.True(nextPrayerTime.Value > DateTime.Today);
        }
        else
        {
            // If no next prayer is found, it might be because all prayers for today have passed
            // This is acceptable behavior
            Assert.Null(nextPrayerTime);
        }
    }
} 