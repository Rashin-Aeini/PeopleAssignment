using System.Collections.Generic;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Models.Services
{
    public interface ILanguageService
    {
        Language Add(CreateLanguageViewModel language);
        List<Language> All();
        Language FindById(int id);
        bool Edit(int id, CreateLanguageViewModel language);
        bool Remove(int id);
    }
}
