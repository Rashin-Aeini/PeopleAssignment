using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeopleAssignment.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<PersonLanguage> People { get; set; }
    }
}
