using System.Collections.Generic;
using PeopleAssignment.Models.Repos;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public class PeopleService : IPeopleService
    {
        private IPeopleRepo Repo { get; }

        public PeopleService(InMemoryPeopleRepo repo)
        {
            Repo = repo;
        }

        public Person Add(CreatePersonViewModel person)
        {
            return Repo.Create(new Person()
            {
                Name = person.Name,
                City = person.City,
                Phone = person.Phone
            });
        }

        public List<Person> All()
        {
            return Repo.Read();
        }

        public bool Remove(int id)
        {
            return Repo.Delete(new Person() { Id = id });
        }

        public bool Edit(int id, CreatePersonViewModel person)
        {
            return Repo.Update(new Person()
            {
                Id = id,
                Name = person.Name,
                City = person.City,
                Phone = person.Phone
            });
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
                if (item.Name.Contains(search) || item.City.Contains(search))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}

