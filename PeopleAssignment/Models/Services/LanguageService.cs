using System.Collections.Generic;
using PeopleAssignment.Models.Repos;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public class LanguageService : ILanguageService
    {
        private ILanguageRepo Repo { get; }

        public LanguageService(ILanguageRepo repo)
        {
            Repo = repo;
        }

        public Language Add(CreateLanguageViewModel language)
        {
            Language entry = new Language()
            {
                Name = language.Name
            };

            return Repo.Create(entry);
        }

        public List<Language> All()
        {
            return Repo.Read();
        }

        public Language FindById(int id)
        {
            return Repo.Read(id);
        }

        public bool Edit(int id, CreateLanguageViewModel language)
        {
            Language entry = new Language()
            {
                Id = id,
                Name = language.Name
            };

            return Repo.Update(entry);
        }

        public bool Remove(int id)
        {
            Language entry = FindById(id);
            return entry != null && Repo.Delete(entry);
        }
    }
}
