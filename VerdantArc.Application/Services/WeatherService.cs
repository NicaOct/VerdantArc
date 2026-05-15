using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;
using VerdantArc.Domain.Entities;

namespace VerdantArc.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRepository<WeatherCache> _weatherCacheRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(
        IHttpClientFactory httpClientFactory,
        IRepository<WeatherCache> weatherCacheRepository,
        IConfiguration configuration,
        ILogger<WeatherService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _weatherCacheRepository = weatherCacheRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<WeatherResponseDto> GetCurrentWeatherAsync()
    {
        var caches = await _weatherCacheRepository.GetAllAsync();
        var latestCache = caches.OrderByDescending(c => c.FetchedAt).FirstOrDefault();
        
        int cacheDurationMinutes = _configuration.GetValue<int>("WeatherApi:CacheDurationMinutes", 30);

        if (latestCache == null || (DateTime.UtcNow - latestCache.FetchedAt).TotalMinutes > cacheDurationMinutes)
        {
            await RefreshWeatherCacheAsync();
            caches = await _weatherCacheRepository.GetAllAsync();
            latestCache = caches.OrderByDescending(c => c.FetchedAt).FirstOrDefault();
        }

        if (latestCache == null)
            throw new Exception("Unable to retrieve weather data.");

        return new WeatherResponseDto
        {
            TemperatureCelsius = latestCache.TemperatureCelsius,
            WindSpeedKmh = latestCache.WindSpeedKmh,
            HumidityPercent = latestCache.HumidityPercent,
            WeatherDescription = latestCache.WeatherDescription,
            FetchedAt = latestCache.FetchedAt
        };
    }

    public async Task RefreshWeatherCacheAsync()
    {
        try
        {
            var baseUrl = _configuration["WeatherApi:BaseUrl"];
            var lat = _configuration["WeatherApi:Latitude"];
            var lon = _configuration["WeatherApi:Longitude"];
            
            var url = $"{baseUrl}?latitude={lat}&longitude={lon}&current=temperature_2m,windspeed_10m,relativehumidity_2m,weathercode&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=Europe/Bucharest&forecast_days=7";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;
            var current = root.GetProperty("current");

            var temp = current.GetProperty("temperature_2m").GetDecimal();
            var wind = current.GetProperty("windspeed_10m").GetDecimal();
            var humidity = current.GetProperty("relativehumidity_2m").GetInt32();
            var code = current.GetProperty("weathercode").GetInt32();

            var cache = new WeatherCache
            {
                FetchedAt = DateTime.UtcNow,
                TemperatureCelsius = temp,
                WindSpeedKmh = wind,
                HumidityPercent = humidity,
                WeatherDescription = MapWeatherCode(code),
                ForecastJson = jsonString
            };

            await _weatherCacheRepository.AddAsync(cache);
            _logger.LogInformation("Weather cache updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh weather cache.");
            // Don't throw, let it use old cache if possible
        }
    }

    private string MapWeatherCode(int code)
    {
        return code switch
        {
            0 => "Clear sky",
            1 or 2 or 3 => "Partly cloudy",
            45 or 48 => "Fog",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            71 or 73 or 75 => "Snow",
            95 or 96 or 99 => "Thunderstorm",
            _ => "Unknown"
        };
    }
}
