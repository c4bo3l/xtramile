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
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> Logger;
        private readonly IWeatherServices WeatherServices;

        public LocationController(IWeatherServices weatherServices, ILogger<LocationController> logger)
        {
            WeatherServices = weatherServices ?? throw new ArgumentNullException(nameof(weatherServices));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                ICollection<Country> countries = await WeatherServices.GetCountries();
                return Ok(countries);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("country/{countryName}/cities")]
        public async Task<IActionResult> GetCities(string countryName)
        {
            try
            {
                if(string.IsNullOrEmpty(countryName?.Trim()))
                {
                    throw new ArgumentNullException(nameof(countryName));
                }

                ICollection<string> cities = await WeatherServices.GetCities(countryName);
                return Ok(cities);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}