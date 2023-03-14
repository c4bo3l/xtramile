using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class WeatherServices : IWeatherServices
    {
        public const string ConfigAPIKey = "WeatherApiKey";
        private readonly IHttpClientServices HttpClientServices;
        private readonly IUtilsServices UtilsServices;
        private readonly WeatherConfig WeatherConfig;

        public WeatherServices(IHttpClientServices httpClientServices, IOptions<WeatherConfig> weatherConfig, IUtilsServices utilsServices)
        {
            HttpClientServices = httpClientServices ?? throw new ArgumentNullException(nameof(httpClientServices));
            UtilsServices = utilsServices ?? throw new ArgumentNullException(nameof(utilsServices));
            WeatherConfig = weatherConfig?.Value ?? throw new ArgumentNullException(nameof(httpClientServices));
        }

        public async Task<WeatherInfo> GetWeatherInfo(string cityName)
        {
            if (string.IsNullOrEmpty(cityName?.Trim()))
            {
                throw new ArgumentNullException(nameof(cityName));
            }

            using(HttpClient client = HttpClientServices.GetWeatherAPIClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"weather?q={cityName}&appid={WeatherConfig.Key}&units=imperial");

                responseMessage.EnsureSuccessStatusCode();

                string message = await responseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeAnonymousType(message, new {
                    coord = new {
                        lon = 0.0m,
                        lat = 0.0m
                    }
                });
                
                responseMessage = await client.GetAsync($"onecall?lat={jsonData.coord.lat}&lon={jsonData.coord.lon}&exclude=hourly,daily,minutely&appid={WeatherConfig.Key}&units=imperial");

                WeatherInfo info = await UtilsServices.ResponseHandler<WeatherInfo>(responseMessage);
                
                info.current.temp_celcius = UtilsServices.FahrenheitToCelcius(info.current.temp);
                
                return info;
            }
        }
    }
}
