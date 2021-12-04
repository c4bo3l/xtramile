using System;

namespace Infrastructure.Services
{
    public class UtilsServices : IUtilsServices
    {
        public decimal FahrenheitToCelcius(decimal fahrenheit)
        {
            return Math.Round((fahrenheit - 32) / 1.8m, 2, MidpointRounding.AwayFromZero);
        }
    }
}