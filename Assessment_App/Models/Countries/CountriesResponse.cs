using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_App.Models
{
   
    public class Name
    {
        public string Common { get; set; }
    }

    public class Countries
    {
        public Name Name { get; set; }
        public List<string> Capital { get; set; }
        public List<string> Borders { get; set; } = new();
    }
}