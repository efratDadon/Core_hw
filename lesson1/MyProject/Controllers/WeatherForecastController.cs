using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IEnumerable<WeatherForecast> _weatherForecasts;


    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _weatherForecasts = GenerateWeatherForecasts();

    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecasts;
        }

        private IEnumerable<WeatherForecast> GenerateWeatherForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
}
