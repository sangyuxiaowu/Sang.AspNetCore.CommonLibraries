using Microsoft.AspNetCore.Mvc;
using Sang.AspNetCore.CommonLibraries.Filter;

namespace WebAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            throw new Exception("≤‚ ‘“Ï≥£");
        }
    }
}
