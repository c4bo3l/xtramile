using System;
using Xunit;

namespace Infrastructure.Services.Tests
{
    public class UtilsServicesTests: IDisposable
    {
        private IUtilsServices UtilsServices;

        public UtilsServicesTests()
        {
            UtilsServices = new UtilsServices();
        }

        public void Dispose()
        {
            if (UtilsServices != null)
            {
                UtilsServices = null;
            }
        }

        [Fact]
        public void FahrenheitToCelciusTests()
        {
            Assert.Equal(-17.78m, UtilsServices.FahrenheitToCelcius(0));
            Assert.Equal(37.78m, UtilsServices.FahrenheitToCelcius(100m));
        }
    }
}