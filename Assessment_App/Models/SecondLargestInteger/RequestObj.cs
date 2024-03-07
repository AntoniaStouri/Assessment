using System.ComponentModel.DataAnnotations;

namespace Assessment_App.Models.SecondLargestInteger
{
    public class RequestObj
    {
        [Required]
        public IEnumerable<int> RequestArrayObj { get; set; }
    }
}
