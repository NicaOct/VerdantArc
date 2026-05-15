namespace VerdantArc.Application.DTOs;

public class WeatherResponseDto
{
    public decimal TemperatureCelsius { get; set; }
    public decimal WindSpeedKmh { get; set; }
    public int HumidityPercent { get; set; }
    public string WeatherDescription { get; set; } = string.Empty;
    public DateTime FetchedAt { get; set; }
}
