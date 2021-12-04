using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Services
{
    public interface ILocationServices
    {
        Task<ICollection<Country>> GetCountries();
        Task<ICollection<string>> GetCities(string countryName);
    }
}