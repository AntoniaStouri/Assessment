using Assessment_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment_App.Services
{
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _client;
        public CountryApiService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Country>> GetCountriesFromApi()
        {
            HttpResponseMessage response = await _client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,borders");

            string jsonContent = await response.Content.ReadAsStringAsync();
            List<Countries> root = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Countries>>(jsonContent);

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
                countries.Add(newCountry);
            }
            return countries;
        }
    }
}