using System;
using Infrastructure.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Tests
{
    public abstract class BaseTest : IDisposable
    {
        IServiceCollection Services;

        ServiceProvider ServiceProvider;

        private IWeatherServices _WeatherServices;
        protected IWeatherServices WeatherServices
        {
            get
            {
                if (_WeatherServices == null)
                {
                    _WeatherServices = ServiceProvider.GetRequiredService<IWeatherServices>(); ;
                }
                return _WeatherServices;
            }
        }

        private ILocationServices _LocationServices;
        protected ILocationServices LocationServices
        {
            get
            {
                if (_LocationServices == null)
                {
                    _LocationServices = ServiceProvider.GetRequiredService<ILocationServices>(); ;
                }
                return _LocationServices;
            }
        }

        private IHttpClientServices _HttpClientServices;
        protected IHttpClientServices HttpClientServices
        {
            get
            {
                if (_HttpClientServices == null)
                {
                    _HttpClientServices = ServiceProvider.GetRequiredService<IHttpClientServices>(); ;
                }
                return _HttpClientServices;
            }
        }

        public BaseTest()
        {
            Services = new ServiceCollection();
            Configure();
            ServiceProvider = Services.BuildServiceProvider();
        }

        private void Configure()
        {
            Services.AddHttpClient();
            Services.Configure<WeatherConfig>((options) => options.Key = "1207e9a1198e7066d22e9c593e7b4c0d");
            Services.AddSingleton<IHttpClientServices, HttpClientServices>();
            Services.AddSingleton<IUtilsServices, UtilsServices>();
            Services.AddSingleton<IWeatherServices, WeatherServices>();
            Services.AddSingleton<ILocationServices, LocationServices>();
        }

        public void Dispose()
        {
            if (_WeatherServices != null)
            {
                _WeatherServices = null;
            }
        }
    }
}