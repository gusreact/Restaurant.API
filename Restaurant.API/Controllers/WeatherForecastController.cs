using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    public class TemperatureRequest()
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.Get(1, 0, 100);
        }

        [HttpGet]
        [Route("{take}/GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetByTake([FromQuery] int max, [FromRoute] int take)
        {
            return _weatherForecastService.Get(take, 0 , max);
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromQuery] int count, [FromBody] TemperatureRequest request)
        {
            if (count < 0 || request.Max < request.Min)
            {
                return BadRequest("Count has to be positive number, and max must be greater than the min value");
            }
            var result = _weatherForecastService.Get(count, request.Min, request.Max);
            return Ok(result);
        }
    }
}
