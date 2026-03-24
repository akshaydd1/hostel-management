using Microsoft.AspNetCore.Mvc;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // GET /api/weatherforecast
        [HttpGet]
        public IActionResult GetWeatherForecast()
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new Models.WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();

            return Ok(forecast);
        }
    }
}
