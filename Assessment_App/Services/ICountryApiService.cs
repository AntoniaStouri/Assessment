using Assessment_App.Models;

namespace Assessment_App.Services
{
    public interface ICountryApiService
    {
        Task<List<Country>> GetCountriesFromApi();
    }
}
