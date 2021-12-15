using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PeopleAssignment.Models.Data;

namespace PeopleAssignment.Models.Repos
{
    public class DatabaseLanguageRepo : ILanguageRepo
    {
        private PeopleContext Context { get; }
        
        //private readonly PeopleContext _context;

        public DatabaseLanguageRepo(PeopleContext context)
        {
            Context = context;
        }

        public List<Language> Read()
        {
            return Context.Languages
                .Include(item => item.People)
                .ThenInclude(item => item.Person)
                .ToList();
        }

        public Language Read(int id)
        {
            return Read().SingleOrDefault(item => item.Id == id);
        }

        public Language Create(Language entry)
        {
            Context.Languages.Add(entry);
            Context.SaveChanges();
            return entry;
        }

        public bool Update(Language entry)
        {
            Context.Languages.Update(entry);
            return Context.SaveChanges() > 0;
        }

        public bool Delete(Language entry)
        {
            Context.Languages.Remove(entry);
            return Context.SaveChanges() > 0;
        }
    }
}
