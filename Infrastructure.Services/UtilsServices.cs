using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class UtilsServices : IUtilsServices
    {
        public decimal FahrenheitToCelcius(decimal fahrenheit)
        {
            return Math.Round((fahrenheit - 32) / 1.8m, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<T> ResponseHandler<T>(HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();

            string jsonData = await responseMessage.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(jsonData);
            return obj;
        }
    }
}