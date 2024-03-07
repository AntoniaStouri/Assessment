using Assessment_App.Models.SecondLargestInteger;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondLargestIntegerController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetSecondLargestInteger([FromBody] RequestObj requestObj)
        {
            List<int> integers = requestObj.RequestArrayObj.ToList();

            if (integers.Count < 2)
            {
                return BadRequest("Array should have at least two integers");
            }

            int secondLargestInteger = integers.OrderByDescending(n => n).Skip(1).FirstOrDefault();

            return Ok(new { SecondLargestInteger = secondLargestInteger });


        }
    }
}