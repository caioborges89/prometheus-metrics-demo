using App.Metrics;
using App.Metrics.Timer;
using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Metrics.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private IMetrics _metrics;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Get All WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        
    }
}
