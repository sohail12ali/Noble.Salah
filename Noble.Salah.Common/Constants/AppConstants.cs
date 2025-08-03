namespace Noble.Salah.Common.Constants;

public struct AppConstants
{
    public struct Images
    {
        public const string Fajr = "_content/Noble.Salah.UI.Shared/Assets/Images/bgfajr.png";
        public const string Sunrise = "_content/Noble.Salah.UI.Shared/Assets/Images/bgsunrise.png";
        public const string Dhuhr = "_content/Noble.Salah.UI.Shared/Assets/Images/bgdhuhr.png";
        public const string Asr = "_content/Noble.Salah.UI.Shared/Assets/Images/bgasr.png";
        public const string Maghrib = "_content/Noble.Salah.UI.Shared/Assets/Images/bgmaghrib.png";
        public const string Isha = "_content/Noble.Salah.UI.Shared/Assets/Images/bgisha.png";
    }

    public struct Formats
    {
        public const string TimeSpanOnly = "c";
    }

    public struct Settings
    {
        public const string CalculationMethod = "calculationMethod";
        public const string SchoolOfThought = "schoolOfThought";
        public const string EnableNotifications = "enableNotifications";
        public const string EnableAdhanAudio = "enableAdhanAudio";
        public const string EnableDarkMode = "enableDarkMode";
    }

    public struct Location
    {
        public const string Latitude = "cachedLatitude";
        public const string Longitude = "cachedLongitude";
        public const string Timezone = "cachedTimezone";
        public const string LastUpdated = "locationLastUpdated";
    }
}