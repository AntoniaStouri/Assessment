using Assessment_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Policy;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;


namespace Assessment_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetCountries()
        {

            var httpClient = new HttpClient();

            HttpResponseMessage response = httpClient.GetAsync(new Uri("https://restcountries.com/v3.1/independent?status=true&fields=name,capital,borders")).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            var countries = JsonConvert.DeserializeObject(responseBody);


            return Ok(countries);
        }

    }
    }




