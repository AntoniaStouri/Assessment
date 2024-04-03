using Assessment_App.Models;

namespace Assessment_App.Repositories
{
    public interface ICountryCacheRepository
    {
        List<Country> GetCountriesFromCache();
        void SaveCountriesToCache(List<Country> countries);
    }
}
