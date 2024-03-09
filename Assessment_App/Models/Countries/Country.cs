using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_App.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        public string CommonName { get; set; }
        public string Capital { get; set; }
        public List<Border> Borders { get; set; } = new();

    }

    public class Border
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BorderId { get; set; }
        [Required]
        public string BorderName { get; set; }
    }
}
