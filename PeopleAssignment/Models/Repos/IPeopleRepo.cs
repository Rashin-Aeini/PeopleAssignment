using System.Collections.Generic;

namespace PeopleAssignment.Models.Repos
{
    public interface IPeopleRepo
    {
        List<Person> Read();
        Person Read(int id);
        Person Create(Person entry);
        bool Update(Person entry);
        bool Delete(Person entry);
    }
}
