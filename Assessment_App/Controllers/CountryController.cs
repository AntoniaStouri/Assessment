using Assessment_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        public async Task<List<Country>> GetCountries()
        {

            string cacheKey = "countriesCacheKey";

            if (_cache.TryGetValue<List<Country>>(cacheKey, out countries))
            {
                countries = _cache.Get<List<Country>>(cacheKey);
                return countries;
            }

            else if (_context.Countries.Any())
            {
                countries = _context.Countries.ToList();
                _cache.Set<List<Country>>(cacheKey, countries, TimeSpan.FromMinutes(1));
                return countries;
            }
            else
            {
                try
                {
                    HttpResponseMessage response = await _client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,borders");
                    response.EnsureSuccessStatusCode();

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
                            Capital = country.Capital.Count >0 ?country.Capital[0] : null,
                            Borders = new List<Border>()
                        };


                        foreach (var borderName in country.Borders)
                        {
                            var border = new Border { BorderName = borderName };
                            newCountry.Borders.Add(border);
                        }

                        _context.Countries.Add(newCountry);
                        _context.SaveChanges();
                        countries = _context.Countries.ToList();
                        _cache.Set<List<Country>>(cacheKey, countries, TimeSpan.FromMinutes(1));
                    }
                    return countries;
                }
                catch (Exception ex)
                {
                    // Handle internal server error
                    Console.WriteLine("Internal Server Error. Please try again later.");
                    // You might want to log the exception for further investigation

                     return null;

                }

            }


        }


    }
}




