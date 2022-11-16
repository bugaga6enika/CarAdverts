using Microsoft.AspNetCore.Mvc;

namespace CarAdverts.Net7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly Summary[] Summaries = new[]
        {
            Summary.Freezing, Summary.Bracing, Summary.Chilly, Summary.Cool, Summary.Mild, Summary.Warm, Summary.Balmy, Summary.Hot, Summary.Sweltering, Summary.Scorching
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
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
}