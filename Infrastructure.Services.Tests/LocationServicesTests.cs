using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Model;
using NSubstitute;
using Xunit;

namespace Infrastructure.Services.Tests
{
    public class LocationServicesTests : BaseTest
    {
        public LocationServicesTests() : base()
        { }

        [Fact]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new LocationServices(null, null));
            Assert.Throws<ArgumentNullException>(() => new LocationServices(HttpClientServices, null));
            Assert.Throws<ArgumentNullException>(() => new LocationServices(null, Substitute.For<IUtilsServices>()));
        }

        [Fact]
        public void GetCountriesTest()
        {
            ICollection<Country> countries = LocationServices.GetCountries().GetAwaiter().GetResult();
            Assert.NotEmpty(countries);
            Assert.Contains(countries, value => value.name == "Indonesia");
        }

        [Fact]
        public void GetCities_NegativeTest()
        {
            Assert.Throws<ArgumentNullException>(() => LocationServices.GetCities(null).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => LocationServices.GetCities(string.Empty).GetAwaiter().GetResult());
            Assert.Throws<ArgumentNullException>(() => LocationServices.GetCities(" ").GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCitiesTest()
        {
            ICollection<string> cities = LocationServices.GetCities("Indonesia").GetAwaiter().GetResult();
            Assert.NotEmpty(cities);
            Assert.Contains("Bandung", cities);
        }
    }
}