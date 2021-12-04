using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Services
{
    public class LocationServices : ILocationServices
    {
        public const string ConfigAPIKey = "WeatherApiKey";
        private readonly IHttpClientServices HttpClientServices;
        private readonly IUtilsServices UtilsServices;

        public LocationServices(IHttpClientServices httpClientServices, IUtilsServices utilsServices)
        {
            HttpClientServices = httpClientServices ?? throw new ArgumentNullException(nameof(httpClientServices));
            UtilsServices = utilsServices ?? throw new ArgumentNullException(nameof(utilsServices));
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
                CityResponse cityResponse = await UtilsServices.ResponseHandler<CityResponse>(responseMessage);
                return cityResponse.data.OrderBy(cityName => cityName.ToLower()).ToArray();
            }
        }

        public async Task<ICollection<Country>> GetCountries()
        {
            using (HttpClient client = HttpClientServices.GetPostmanAPIClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("countries/info?returns=name");
                CountryResponse countryResponse = await UtilsServices.ResponseHandler<CountryResponse>(responseMessage);
                return countryResponse.data.OrderBy(country => country.name).ToArray();
            }
        }
    }
}