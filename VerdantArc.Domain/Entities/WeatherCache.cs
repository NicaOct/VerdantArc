namespace VerdantArc.Domain.Entities;

public class WeatherCache
{
    public int Id { get; set; }
    public DateTime FetchedAt { get; set; } = DateTime.UtcNow;
    public decimal TemperatureCelsius { get; set; }
    public decimal WindSpeedKmh { get; set; }
    public int HumidityPercent { get; set; }
    public string WeatherDescription { get; set; } = string.Empty;
    public string ForecastJson { get; set; } = string.Empty;
}
