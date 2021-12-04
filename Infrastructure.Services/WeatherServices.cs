using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<ICollection<string>> GetCities(string countryName)
        {
            if (string.IsNullOrEmpty(countryName?.Trim()))
            {
                throw new ArgumentNullException(nameof(countryName));
            }

            using (HttpClient client = HttpClientServices.GetPostmanAPIClient())
            {
                StringContent body = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(new
                    {
                        country = countryName
                    }),
                    Encoding.UTF8,
                    Application.Json
                );
                HttpResponseMessage responseMessage = await client.PostAsync("countries/cities", body);
                CityResponse cityResponse = await ResponseHandler<CityResponse>(responseMessage);
                return cityResponse.data.OrderBy(cityName => cityName.ToLower()).ToArray();
            }
        }

        public async Task<ICollection<Country>> GetCountries()
        {
            using (HttpClient client = HttpClientServices.GetPostmanAPIClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("countries/info?returns=name");
                CountryResponse countryResponse = await ResponseHandler<CountryResponse>(responseMessage);
                return countryResponse.data.OrderBy(country => country.name).ToArray();
            }
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

                WeatherInfo info = await ResponseHandler<WeatherInfo>(responseMessage);
                
                info.current.temp_celcius = UtilsServices.FahrenheitToCelcius(info.current.temp);
                
                return info;
            }
        }

        private async Task<T> ResponseHandler<T>(HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();

            string jsonData = await responseMessage.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(jsonData);
            return obj;
        }
    }
}
