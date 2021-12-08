using System.Collections.Generic;
using System.Linq;
using PeopleAssignment.Models.Data;

namespace PeopleAssignment.Models.Repos
{
    public class DatabasePeopleRepo : IPeopleRepo
    {
        private PeopleContext Context { get; }

        public DatabasePeopleRepo(PeopleContext context)
        {
            Context = context;
        }

        public List<Person> Read()
        {
            return Context.Persons.ToList();
        }

        public Person Read(int id)
        {
            return Read().SingleOrDefault(item => item.Id == id);
        }

        public Person Create(Person entry)
        {
            Context.Add(entry);

            Context.SaveChanges();

            return entry;
        }

        public bool Update(Person entry)
        {
            Context.Persons.Update(entry);

            int rows = Context.ChangeTracker.Entries().Count();

            int effects = Context.SaveChanges();

            return rows == effects;
        }

        public bool Delete(Person entry)
        {
            Context.Persons.Remove(entry);

            int rows = Context.ChangeTracker.Entries().Count();

            int effects = Context.SaveChanges();

            return rows == effects;
        }
    }
}
