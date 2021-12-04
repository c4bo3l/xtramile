using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Services
{
    public interface IWeatherServices
    {
        Task<WeatherInfo> GetWeatherInfo(string cityName);
    }
}