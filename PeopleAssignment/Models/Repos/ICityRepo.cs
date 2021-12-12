using System.Collections.Generic;

namespace PeopleAssignment.Models.Repos
{
    public interface ICityRepo
    {
        List<City> Read();
        City Read(int id);
        City Create(City entry);
        bool Update(City entry);
        bool Delete(City entry);
    }
}
