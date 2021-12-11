using System.Collections.Generic;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public interface ICountryService
    {
        Country Add(CreateCountryViewModel country);
        List<Country> All();
        Country FindById(int id);
        bool Edit(int id, CreateCountryViewModel country);
        bool Remove(int id);
    }
}
