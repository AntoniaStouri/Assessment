using Assessment_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;



namespace Assessment_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;
        private List<Countries> root = new();

        public CountryController(ILogger<CountryController> logger, IMemoryCache memoryCache, AppDbContext context)
        {
            _cache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public async Task<List<Country>> GetCountries()
        {

            List<Country> countries = new List<Country>();
            string key = "countriesCacheKey";


            if (_cache.TryGetValue<List<Country>>(key, out countries))
            {
                return countries;
            }

            if (_context.Countries.Any())
            {
                countries = _context.Countries.ToList();
                return countries;


            }
            else
            {


                using (HttpClient client = new HttpClient())
                {
                    // Make the API request
                    HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/independent?status=true&fields=name,capital,borders");


                    // Read the response content
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Countries> root = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Countries>>(jsonContent);


                    //_cache.Set<List<Country>>(key, countries, TimeSpan.FromMinutes(3));


                    // Create a new country
                    foreach (Countries country in root)
                    {
                        var newCountry = new Country
                        {
                            CommonName = country.Name.Common,
                            Capital = country.Capital.ToString(),
                            Borders = new List<Border>()
                        };
                        foreach (var borderName in country.Borders)
                        {
                            var border = new Border { BorderName = borderName };
                            newCountry.Borders.Add(border);
                        }
                        countries.Add(newCountry);
                        _context.Countries.Add(newCountry);


                        _context.SaveChanges();
                    }
                    _cache.Set<List<Country>>(key, countries, TimeSpan.FromMinutes(3));
                    return countries;
                }
            }


            // Access the data using the model



            //return countries;




        }


        }
    }





 

