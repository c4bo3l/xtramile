using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Model;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Infrastructure.Services.Tests
{
    public class WeatherServicesTests : BaseTest
    {
        public WeatherServicesTests() : base()
        { }

        [Fact]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new WeatherServices(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new WeatherServices(HttpClientServices, null, null));
            Assert.Throws<ArgumentNullException>(() => new WeatherServices(HttpClientServices, Substitute.For<IOptions<WeatherConfig>>(), null));
        }

        [Fact]
        public void GetCountriesTest()
        {
            ICollection<Country> countries = WeatherServices.GetCountries().GetAwaiter().GetResult();
            Assert.NotEmpty(countries);
            Assert.True(countries.Any(country => country.name == "Indonesia"));
        }

        [Fact]
        public void GetCities_NegativeTest()
        {
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetCities(null).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetCities(string.Empty).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetCities(" ").GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCitiesTest()
        {
            ICollection<string> cities = WeatherServices.GetCities("Indonesia").GetAwaiter().GetResult();
            Assert.NotEmpty(cities);
            Assert.Contains("Bandung", cities);
        }

        [Fact]
        public void GetWeatherInfo_NegativeTest()
        {
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetWeatherInfo(null).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetWeatherInfo(string.Empty).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => WeatherServices.GetWeatherInfo(" ").GetAwaiter().GetResult());
        }

        [Fact]
        public void GetWeatherInfoTest()
        {
            WeatherInfo weatherInfo = WeatherServices.GetWeatherInfo("Bandung").GetAwaiter().GetResult();
            Assert.NotNull(weatherInfo);
        }
    }
}