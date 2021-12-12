using System.Collections.Generic;

namespace PeopleAssignment.Models.ViewModels
{
    public class PeopleViewModel
    {
        public PeopleViewModel()
        {
            Result = new List<Person>();
        }

        public string Search { get; set; }
        public List<Person> Result { get; }
    }
}
