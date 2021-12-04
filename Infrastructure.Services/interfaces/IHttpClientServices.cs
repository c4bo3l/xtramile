using System.Net.Http;

namespace Infrastructure.Services
{
    public interface IHttpClientServices
    {
        HttpClient GetWeatherAPIClient();
        HttpClient GetPostmanAPIClient();
    }
}