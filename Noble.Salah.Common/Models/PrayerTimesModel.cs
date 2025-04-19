namespace Noble.Salah.Common.Models;

public record PrayerTimesModel
{
    public PrayerTimesModel(DateTime fajz, DateTime sunrise, DateTime dhuhr, DateTime asr, DateTime maghrib, DateTime isha)
    {
        this.Fajr = fajz;
        this.Sunrise = sunrise;
        this.Dhuhr = dhuhr;
        this.Asr = asr;
        this.Maghrib = maghrib;
        this.Isha = isha;
    }

    public DateTime Fajr { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Dhuhr { get; set; }
    public DateTime Asr { get; set; }
    public DateTime Maghrib { get; set; }
    public DateTime Isha { get; set; }
}
