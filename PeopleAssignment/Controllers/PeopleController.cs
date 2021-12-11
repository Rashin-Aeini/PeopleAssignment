using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    public class PeopleController : Controller
    {
        private IPeopleService Service { get; }

        public PeopleController(IPeopleService service)
        {
            Service = service;
        }
        public IActionResult Index(string search)
        {
            List<Person> people = !string.IsNullOrEmpty(search) ?
                Service.Search(search) :
                Service.All();

            List<PeopleViewModel> model = new List<PeopleViewModel>();
            /*
            foreach (Person item in people)
            {
                model.Add(new PeopleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    City = item.City
                });
            }
            */

            return View(model);
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
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreatePersonViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            Service.Add(entry);
            return RedirectToAction("Index");
        }
    }
}
