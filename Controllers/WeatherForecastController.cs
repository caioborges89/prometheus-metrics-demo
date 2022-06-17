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

    [HttpGet("/get-all")]
    public IActionResult Get()
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Get All WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var response = Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var result = Random.Shared.Next(1, 15);

            if (result < 10)
                return Ok(response);
            else if (result >= 10 && result < 13)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPost("/save")]
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
            var response = new WeatherForecastModel
            {
                Date = DateTime.Now,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                TemperatureC = Random.Shared.Next(-20, 55),
            };

            var result = Random.Shared.Next(1, 15);

            if (result < 10)
                return Ok(response);
            else if (result >= 10 && result < 13)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
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
            var result = Random.Shared.Next(1, 15);

            if (result < 10)
                return NoContent();
            else if (result >= 10 && result < 13)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPatch("/patch")]
    public IActionResult Patch(WeatherForecastModel model)
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Patch WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var response = new WeatherForecastModel
            {
                Date = DateTime.Now,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                TemperatureC = Random.Shared.Next(-20, 55),
            };

            var result = Random.Shared.Next(1, 15);

            if (result < 10)
                return Ok(response);
            else if (result >= 10 && result < 13)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpDelete("/delete")]
    public IActionResult Delete(int id)
    {
        var weatherForecastTimer = new TimerOptions
        {
            Name = "Delete WeatherForecast",
            MeasurementUnit = Unit.Commands,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds
        };

        using (_metrics.Measure.Timer.Time(weatherForecastTimer))
        {
            var result = Random.Shared.Next(1, 15);

            if (result < 10)
                return NoContent();
            else if (result >= 10 && result < 13)
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
}
