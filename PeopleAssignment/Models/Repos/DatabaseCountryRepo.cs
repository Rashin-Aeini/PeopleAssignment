using System.Collections.Generic;
using System.Linq;
using PeopleAssignment.Models.Data;

namespace PeopleAssignment.Models.Repos
{
    public class DatabaseCountryRepo : ICountryRepo
    {
        private PeopleContext Context { get; }

        public DatabaseCountryRepo(PeopleContext context)
        {
            Context = context;
        }

        public List<Country> Read()
        {
            return Context.Countries.ToList();
        }

        public Country Read(int id)
        {
            return Read().SingleOrDefault(item => item.Id == id);
        }

        public Country Create(Country entry)
        {
            Context.Countries.Add(entry);
            Context.SaveChanges();
            return entry;
        }

        public bool Update(Country entry)
        {
            Context.Countries.Update(entry);
            return Context.SaveChanges() > 0;
        }

        public bool Delete(Country entry)
        {
            Context.Countries.Remove(entry);
            return Context.SaveChanges() > 0;
        }
    }
}
