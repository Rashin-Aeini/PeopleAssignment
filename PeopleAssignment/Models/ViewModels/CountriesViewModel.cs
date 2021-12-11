using System.Collections.Generic;

namespace PeopleAssignment.Models.ViewModels
{
    public class CountriesViewModel
    {
        public CountriesViewModel()
        {
            Countries = new List<Country>();
        }

        public string Search { get; set; }
        public List<Country> Countries { get; }
    }
}
