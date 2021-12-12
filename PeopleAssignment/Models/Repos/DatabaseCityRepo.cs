using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PeopleAssignment.Models.Data;

namespace PeopleAssignment.Models.Repos
{
    public class DatabaseCityRepo : ICityRepo
    {
        private PeopleContext Context { get; }

        public DatabaseCityRepo(PeopleContext context)
        {
            Context = context;
        }

        public List<City> Read()
        {
            return Context.Cities.Include(item => item.Country).ToList();
        }

        public City Read(int id)
        {
            return Read().SingleOrDefault(item => item.Id == id);
        }

        public City Create(City entry)
        {
            Context.Cities.Add(entry);
            Context.SaveChanges();
            return entry;
        }

        public bool Update(City entry)
        {
            Context.Cities.Update(entry);
            return Context.SaveChanges() > 0;
        }

        public bool Delete(City entry)
        {
            Context.Cities.Remove(entry);
            return Context.SaveChanges() > 0;
        }
    }
}
