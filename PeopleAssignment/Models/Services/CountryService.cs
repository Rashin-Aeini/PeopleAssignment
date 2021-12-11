using System.Collections.Generic;
using PeopleAssignment.Models.Repos;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepo Repo { get; }

        public CountryService(ICountryRepo repo)
        {
            Repo = repo;
        }

        public Country Add(CreateCountryViewModel country)
        {
            Country entry = new Country()
            {
                Name = country.Name
            };

            return Repo.Create(entry);
        }

        public List<Country> All()
        {
            return Repo.Read();
        }

        public Country FindById(int id)
        {
            return Repo.Read(id);
        }

        public bool Edit(int id, CreateCountryViewModel country)
        {
            Country entry = new Country()
            {
                Id = id,
                Name = country.Name
            };

            return Repo.Update(entry);
        }

        public bool Remove(int id)
        {
            Country entry = FindById(id);
            return entry != null && Repo.Delete(entry);
        }
    }
}
