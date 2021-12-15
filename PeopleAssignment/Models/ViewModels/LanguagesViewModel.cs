using System.Collections.Generic;

namespace PeopleAssignment.Models.ViewModels
{
    public class LanguagesViewModel
    {
        public LanguagesViewModel()
        {
            Result = new List<Language>();
        }

        public string Search { get; set; }
        public List<Language> Result { get; }
    }
}
