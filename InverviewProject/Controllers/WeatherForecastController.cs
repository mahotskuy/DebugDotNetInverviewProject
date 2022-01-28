using InverviewProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InverviewProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeathereForecaseService _wethereForecaseService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeathereForecaseService wethereForecaseService)
        {
            _logger = logger;
            _wethereForecaseService = wethereForecaseService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _wethereForecaseService.GetForcastAsync();
        }
    }
}
