using System;
using System.Net.Http;

namespace Infrastructure.Services
{
    public class HttpClientServices : IHttpClientServices
    {
        private readonly IHttpClientFactory ClientFactory;

        public HttpClientServices(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public HttpClient GetWeatherAPIClient()
        {
            HttpClient client = ClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
            return client;
        }

        public HttpClient GetPostmanAPIClient()
        {
            HttpClient client = ClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://countriesnow.space/api/v0.1/");
            return client;
        }
    }
}