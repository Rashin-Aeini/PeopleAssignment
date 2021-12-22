using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    [Authorize(Roles = "visitor, admin")]
    public class PeopleController : Controller
    {
        private IPeopleService Service { get; }
        private ICityService CityService { get; }

        public PeopleController(IPeopleService service, ICityService cityService)
        {
            Service = service;
            CityService = cityService;
        }
        public IActionResult Index(PeopleViewModel entry)
        {
            List<Person> people = !string.IsNullOrEmpty(entry.Search) ?
                Service.Search(entry.Search) :
                Service.All();

            entry.Result.AddRange(people);

            return View(entry);
        }

        public IActionResult Details(int id)
        {
            Person model = Service.FindById(id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Create()
        {
            CreatePersonViewModel entry = new CreatePersonViewModel()
            {
                Cities = CityService.All()
            };

            return View(entry);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreatePersonViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                entry.Cities = CityService.All();
                return View(entry);
            }

            Service.Add(entry);
            return RedirectToAction("Index");
        }

        public IActionResult LanguageCoverage(int id)
        {
            LanguageCoverageViewModel entry = Service.LanguageCoverage(id);

            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(entry);

        }

        public IActionResult AddLanguage(int person, int language)
        {
            Service.AddLanguage(person, language);
            return RedirectToAction(nameof(LanguageCoverage), new { Id = person });
        }

        public IActionResult RemoveLanguage(int person, int language)
        {
            Service.RemoveLanguage(person, language);
            return RedirectToAction(nameof(LanguageCoverage), new { Id = person });
        }
    }
}
