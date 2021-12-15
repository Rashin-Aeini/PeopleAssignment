using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    public class LanguageController : Controller
    {
        private ILanguageService Service { get; }

        public LanguageController(ILanguageService service)
        {
            Service = service;
        }

        public IActionResult Index(LanguagesViewModel entry)
        {
            entry.Result.AddRange(Service.All());

            return View(entry);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreateLanguageViewModel entry)
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
            Language entry = Service.FindById(id);
            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(entry);
        }

        public IActionResult Edit(int id)
        {
            Language entry = Service.FindById(id);

            if (entry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CreateLanguageViewModel model = new CreateLanguageViewModel()
            {
                Name = entry.Name
            };

            ViewBag.Id = entry.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateLanguageViewModel entry)
        {
            if (ModelState.IsValid)
            {
                if (Service.Edit(id, entry))
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("NotSave", "Unable to save changes");
            }

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
