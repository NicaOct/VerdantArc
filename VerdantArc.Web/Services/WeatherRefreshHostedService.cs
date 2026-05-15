using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Services;

public class WeatherRefreshHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WeatherRefreshHostedService> _logger;

    public WeatherRefreshHostedService(IServiceProvider serviceProvider, ILogger<WeatherRefreshHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();
                await weatherService.RefreshWeatherCacheAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing weather cache in background service.");
            }

            await Task.Delay(TimeSpan.FromMinutes(25), stoppingToken);
        }
    }
}
