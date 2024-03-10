using Assessment_App.Models;
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
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;
        private readonly HttpClient _client;
        private List<Country> countries = new List<Country>();
        private List<Countries> root = new();

        public CountryController(IMemoryCache memoryCache, AppDbContext context, HttpClient client)
        {
            _cache = memoryCache;
            _context = context;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {

            string cacheKey = "countriesCacheKey";

            if (_cache.TryGetValue<List<Country>>(cacheKey, out countries))
            {
                //get countries from memory cache
                countries = _cache.Get<List<Country>>(cacheKey);
                return Ok(countries);
            }

            else if (_context.Countries.Any())
            {
                //get countries from db
                countries = _context.Countries.Include(c=> c.Borders).ToList();
                //save countries to memory cache
                _cache.Set<List<Country>>(cacheKey, countries, TimeSpan.FromMinutes(1));
                return Ok(countries);
            }
            else
            {
                try
                {
                    //get countries from api
                    HttpResponseMessage response = await _client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,borders");

                    // Read the response content
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Countries> root = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Countries>>(jsonContent);

                    // Create a new country
                    List<Country> countries = new List<Country>();

                    foreach (Countries country in root)
                    {

                        var newCountry = new Country
                        {
                            CommonName = country.Name.Common,
                            Capital = country.Capital.Count > 0 ? country.Capital[0] : null,
                            Borders = new List<Border>()
                        };


                        foreach (var borderName in country.Borders)
                        {
                            var border = new Border { BorderName = borderName };
                            newCountry.Borders.Add(border);
                        }

                        //save countries to db
                        _context.Countries.Add(newCountry);
                        _context.SaveChanges();
                        countries = _context.Countries.ToList();
                        //save countries to memory cache
                        _cache.Set<List<Country>>(cacheKey, countries, TimeSpan.FromMinutes(1));
                    }
                    return Ok(countries);
                }
                catch (Exception ex)
                {
                    //error handling
                    return StatusCode(500, new { error = "Internal Server Error" });

                }

            }
        }
    }
}




