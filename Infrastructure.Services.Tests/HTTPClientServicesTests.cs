using System;
using System.Net.Http;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace Infrastructure.Services.Tests
{
    public class HTTPClientServicesTests
    {
        [Fact]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpClientServices(null));
        }

        [Fact]
        public void GetPostmanAPIClientTest()
        {
            var handler = new Mock<HttpMessageHandler>();
            var factory = handler.CreateClientFactory();

            var service = new HttpClientServices(factory);
            var client = service.GetPostmanAPIClient();

            Assert.NotNull(client);
        }

        [Fact]
        public void GetWeatherAPIClientTest()
        {
            var handler = new Mock<HttpMessageHandler>();
            var factory = handler.CreateClientFactory();

            var service = new HttpClientServices(factory);
            var client = service.GetWeatherAPIClient();

            Assert.NotNull(client);
        }
    }
}
