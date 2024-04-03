using Assessment_App.Models;
using Assessment_App.Repositories;
using Assessment_App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;



namespace Assessment_App.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryApiService _countryService;
        private readonly ICountryCacheRepository _cacheCountryRepository;
        private readonly ICountryDbRepository _dbCountryRepository;

        public CountryController(ICountryApiService countryService, ICountryCacheRepository cacheCountryRepository, ICountryDbRepository dbCountryRepository)
        {
            _countryService = countryService;
            _cacheCountryRepository = cacheCountryRepository;
            _dbCountryRepository = dbCountryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = _cacheCountryRepository.GetCountriesFromCache();
                if (countries == null || countries.Count == 0)
                {
                    countries = await _countryService.GetCountriesFromApi();
                    _cacheCountryRepository.SaveCountriesToCache(countries);
                    _dbCountryRepository.SaveCountriesToDb(countries);
                }
                return Ok(countries);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}




