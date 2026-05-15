using VerdantArc.Application.DTOs;

namespace VerdantArc.Application.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponseDto> GetCurrentWeatherAsync();
    Task RefreshWeatherCacheAsync();
}
