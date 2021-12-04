using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Model;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace xtramile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> Logger;
        private readonly IWeatherServices WeatherServices;

        public WeatherController(IWeatherServices weatherServices, ILogger<WeatherController> logger)
        {
            Logger = logger;
            WeatherServices = weatherServices ?? throw new ArgumentNullException(nameof(weatherServices));
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> Get(string cityName)
        {
            try
            {
                if (string.IsNullOrEmpty(cityName?.Trim()))
                {
                    throw new ArgumentNullException(nameof(cityName));
                }
                WeatherInfo weatherInfo = await WeatherServices.GetWeatherInfo(cityName);
                return Ok(weatherInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
