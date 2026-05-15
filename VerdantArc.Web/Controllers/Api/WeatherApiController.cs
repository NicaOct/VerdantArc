using Microsoft.AspNetCore.Mvc;
using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Controllers.Api;

[ApiController]
[Route("api/v1/weather")]
public class WeatherApiController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherApiController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<WeatherResponseDto>> GetCurrentWeather()
    {
        try
        {
            var weather = await _weatherService.GetCurrentWeatherAsync();
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Unable to fetch weather data." });
        }
    }
}
