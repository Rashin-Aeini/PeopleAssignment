using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    public class CityController : Controller
    {
        private ICityService Service { get; }
        private ICountryService CountryService { get; }

        public CityController(ICityService service, ICountryService countryService)
        {
            Service = service;
            CountryService = countryService;
        }

        public IActionResult Index(CitiesViewModel entry)
        {
            entry.Result.AddRange(Service.All());
            return View(entry);
        }

        public IActionResult Create()
        {
            CreateCityViewModel entry = new CreateCityViewModel()
            {
                Countries = CountryService.All()
            };

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCityViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                entry.Countries = CountryService.All();
                return View(entry);
            }

            Service.Add(entry);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            City entry = Service.FindById(id);
            
            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(entry);
        }

        public IActionResult Edit(int id)
        {
            City entry = Service.FindById(id);

            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CreateCityViewModel model = new CreateCityViewModel()
            {
                Name = entry.Name,
                CountryId = entry.CountryId,
                Countries = CountryService.All()
            };

            ViewBag.Id = entry.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateCityViewModel entry)
        {
            if (ModelState.IsValid)
            {
                if (Service.Edit(id, entry))
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("NotSave", "Unable to save changes");
            }

            entry.Countries = CountryService.All();
            ViewBag.Id = id;

            return View(entry);
        }

        public IActionResult Delete(int id)
        {
            Service.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
