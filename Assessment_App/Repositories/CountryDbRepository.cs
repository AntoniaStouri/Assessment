using Assessment_App.Models;
using Assessment_App.Services;
using Microsoft.EntityFrameworkCore;

namespace Assessment_App.Repositories
{
    public class CountryDbRepository : ICountryDbRepository
    {
        private readonly AppDbContext _context;
        public CountryDbRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Country> GetCountriesFromDb()
        {
            List<Country> countries = new List<Country>();

            if (_context.Countries.Any())
            {
                countries = _context.Countries.Include(c => c.Borders).ToList();
            }
            return countries;
               
        }

        public void SaveCountriesToDb(List<Country> countries)
        {
            _context.Countries.AddRange(countries);
            _context.SaveChanges();
        }

    }
}