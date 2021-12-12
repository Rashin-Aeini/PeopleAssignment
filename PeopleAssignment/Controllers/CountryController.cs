using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    public class CountryController : Controller
    {
        private ICountryService Service { get; }

        public CountryController(ICountryService service)
        {
            Service = service;
        }

        public IActionResult Index(CountriesViewModel entry)
        {
            entry.Countries.AddRange(Service.All());

            return View(entry);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreateCountryViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            Service.Add(entry);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Country entry = Service.FindById(id);
            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(entry);
        }

        public IActionResult Edit(int id)
        {
            Country entry = Service.FindById(id);

            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CreateCountryViewModel model = new CreateCountryViewModel()
            {
                Name = entry.Name
            };

            ViewBag.Id = entry.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateCountryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                if (Service.Edit(id, entry))
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("NotSave", "Unable to save changes");
            }
            
            return View(entry);
        }

        public IActionResult Delete(int id)
        {
            Service.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
