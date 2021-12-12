using System.Collections.Generic;
using PeopleAssignment.Models.Repos;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public class CityService : ICityService
    {
        private ICityRepo Repo { get; }

        public CityService(ICityRepo repo)
        {
            Repo = repo;
        }

        public City Add(CreateCityViewModel city)
        {
            City entry = new City()
            {
                Name = city.Name,
                CountryId = city.CountryId
            };

            return Repo.Create(entry);
        }

        public List<City> All()
        {
            return Repo.Read();
        }

        public City FindById(int id)
        {
            return Repo.Read(id);
        }

        public bool Edit(int id, CreateCityViewModel city)
        {
            City entry = new City()
            {
                Id = id,
                Name = city.Name,
                CountryId = city.CountryId
            };

            return Repo.Update(entry);
        }

        public bool Remove(int id)
        {
            City entry = FindById(id);
            return entry != null && Repo.Delete(entry);
        }
    }
}
