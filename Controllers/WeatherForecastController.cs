using Microsoft.AspNetCore.Mvc;
using WebApiCiCd.Models;
using WebApiCiCd.Services;

namespace WebApiCiCd.Controllers
{
    // Контроллер
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Getting weather forecast");
            try
            {
                var forecast = _weatherService.GetForecast();
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting weather forecast");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            if (id < 0 || id > 4)
                return NotFound();

            var forecast = _weatherService.GetForecast().ElementAt(id);
            return Ok(forecast);
        }
    }
}
