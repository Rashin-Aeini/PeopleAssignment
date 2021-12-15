using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PeopleAssignment.Models.Repos;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public class PeopleService : IPeopleService
    {
        private IPeopleRepo Repo { get; }
        private ILanguageRepo LanguageRepo { get; }

        public PeopleService(IPeopleRepo repo, ILanguageRepo languageRepo)
        {
            Repo = repo;
            LanguageRepo = languageRepo;
        }

        public Person Add(CreatePersonViewModel person)
        {
            Person entry = new Person()
            {
                Name = person.Name,
                CityId = person.CityId,
                Phone = person.Phone
            };
            return Repo.Create(entry);
        }

        public List<Person> All()
        {
            return Repo.Read();
        }

        public bool Remove(int id)
        {
            return Repo.Delete(new Person() { Id = id });
        }

        public LanguageCoverageViewModel LanguageCoverage(int id)
        {
            LanguageCoverageViewModel result = null;

            Person person = FindById(id);

            if (person != null)
            {
                result = new LanguageCoverageViewModel(person);

                /*
                foreach (Language language in LanguageRepo.Read())
                {
                    if (language.People.Any(item => item.PersonId == id))
                    {
                        result.Current.Add(language);
                    }
                    else
                    {
                        result.Possible.Add(language);
                    }
                }
                */

                List<Language> languages = LanguageRepo.Read();

                foreach (PersonLanguage language in person.Languages)
                {
                    Language item = languages
                        .Single(lang => lang.Id == language.LanguageId);

                    result.Current.Add(item);
                    languages.Remove(item);
                }

                result.Possible.AddRange(languages);
            }


            return result;
        }

        public bool AddLanguage(int id, int language)
        {
            bool result = false;

            Person person = FindById(id);

            Language lang = LanguageRepo.Read(language);

            if (person != null && lang != null)
            {
                PersonLanguage newLanguage = new PersonLanguage()
                {
                    PersonId = id,
                    LanguageId = language
                };

                person.Languages.Add(newLanguage);

                result = Repo.Update(person);
            }

            return result;
        }

        public bool RemoveLanguage(int id, int language)
        {
            bool result = false;

            Person person = FindById(id);

            Language lang = LanguageRepo.Read(language);

            if (person != null && lang != null)
            {
                PersonLanguage oldLanguage = null;

                foreach (PersonLanguage item in person.Languages)
                {
                    if (item.LanguageId == language)
                    {
                        oldLanguage = item;
                        break;
                    }
                }

                if (oldLanguage != null)
                {
                    person.Languages.Remove(oldLanguage);

                    result = Repo.Update(person);
                }
                
            }

            return result;
        }

        public bool Edit(int id, CreatePersonViewModel person)
        {
            Person entry = new Person()
            {
                Id = id,
                Name = person.Name,
                CityId = person.CityId,
                Phone = person.Phone
            };

            return Repo.Update(entry);
        }

        public Person FindById(int id)
        {
            return Repo.Read(id);
        }

        public List<Person> Search(string search)
        {
            List<Person> result = new List<Person>();

            foreach (Person item in All())
            {
                if (item.Name.Contains(search) || item.City.Name.Contains(search))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}

