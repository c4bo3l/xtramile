using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Services
{
    public interface IWeatherServices
    {
        Task<ICollection<Country>> GetCountries();
        Task<ICollection<string>> GetCities(string countryName);
        Task<WeatherInfo> GetWeatherInfo(string cityName);
    }
}