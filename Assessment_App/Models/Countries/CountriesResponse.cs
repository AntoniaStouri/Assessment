using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_App.Models
{
    public class NativeName

    {

        public Dictionary<string, string> ell { get; set; }

        public Dictionary<string, string> tur { get; set; }

    }



    public class Name

    {

        public string Common { get; set; }

        public string Official { get; set; }

        public NativeName NativeName { get; set; }

    }



    public class Countries

    {

        public Name Name { get; set; }

        public List<string> Capital { get; set; }

        public List<string> Borders { get; set; }

    }



    public class Root

    {

        public List<Countries> roots { get; set; }

    }
}