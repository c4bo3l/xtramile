using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUtilsServices
    {
        decimal FahrenheitToCelcius(decimal fahrenheit);
        Task<T> ResponseHandler<T>(HttpResponseMessage responseMessage);
    }
}