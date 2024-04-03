using Assessment_App.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;

namespace Assessment_App.Repositories
{
    public class CountryCacheRepository : ICountryCacheRepository
    {
        private readonly IMemoryCache _cache;
        private readonly ICountryDbRepository _dbCountryRepository;
        private string cacheKey = "countriesCacheKey";

        public CountryCacheRepository(IMemoryCache cache, ICountryDbRepository dbCountryRepository)
        {
            _cache = cache;
            _dbCountryRepository = dbCountryRepository;
        }

        public List<Country> GetCountriesFromCache()
        {

            if (_cache.TryGetValue<List<Country>>(cacheKey, out List<Country> countries))
            {
                countries = _cache.Get<List<Country>>(cacheKey);
            }
            else
            {
                countries = _dbCountryRepository.GetCountriesFromDb();
                if (countries.Count > 0)
                {
                    SaveCountriesToCache(countries);
                }
            }
            return countries;
        }

        public void SaveCountriesToCache(List<Country> countries)
        {
            _cache.Set<List<Country>>(cacheKey, countries, TimeSpan.FromMinutes(1));
        }
    }
}