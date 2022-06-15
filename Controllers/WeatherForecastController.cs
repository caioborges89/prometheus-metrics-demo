using App.Metrics;
using App.Metrics.Timer;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Metrics.Demo.Model;

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
    public IEnumerable<WeatherForecastModel> Get()
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Get All WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost(Name = "SaveWeatherForecast")]
    public IActionResult Post(WeatherForecastModel model)
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Save WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var result = new WeatherForecastModel
            {
                Date = DateTime.Now,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                TemperatureC = Random.Shared.Next(-20, 55),
            };

            return Ok(result);
        }
    }

    [HttpPut("/update")]
    public IActionResult Put(int id, WeatherForecastModel model)
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Update WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {            
            return NoContent();
        }
    }

    [HttpPost("/post-error")]
    public IActionResult PostError(WeatherForecastModel model)
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Save WeatherForecast Error",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var result = Random.Shared.Next(1, 10);

            if (result < 5)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("/get-error")]
    public IActionResult GetError()
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Get WeatherForecast Error",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var result = Random.Shared.Next(1, 10);

            if (result < 5)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
}
