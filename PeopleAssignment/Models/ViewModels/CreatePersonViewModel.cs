using System.ComponentModel.DataAnnotations;

namespace PeopleAssignment.Models.ViewModels
{
    public class CreatePersonViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
