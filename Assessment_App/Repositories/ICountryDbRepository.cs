using Assessment_App.Models;

namespace Assessment_App.Repositories
{
    public interface ICountryDbRepository
    {
        List<Country> GetCountriesFromDb();
        void SaveCountriesToDb(List<Country> countries);
    }
}
