using System.ComponentModel.DataAnnotations;

namespace PeopleAssignment.Models.ViewModels
{
    public class CreateCountryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
