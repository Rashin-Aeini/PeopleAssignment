using System.Collections.Generic;

namespace PeopleAssignment.Models.ViewModels
{
    public class LanguageCoverageViewModel
    {
        public Person Person { get; }
        public List<Language> Current { get; }
        public List<Language> Possible { get; }

        public LanguageCoverageViewModel(Person person)
        {
            Person = person;
            Current = new List<Language>();
            Possible = new List<Language>();
        }

    }
}
