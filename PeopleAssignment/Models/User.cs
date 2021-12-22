using System;
using Microsoft.AspNetCore.Identity;

namespace PeopleAssignment.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
