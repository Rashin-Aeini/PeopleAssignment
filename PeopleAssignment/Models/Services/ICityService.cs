using System.Collections.Generic;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public interface ICityService
    {
        City Add(CreateCityViewModel city);
        List<City> All();
        City FindById(int id);
        bool Edit(int id, CreateCityViewModel city);
        bool Remove(int id);
    }
}
