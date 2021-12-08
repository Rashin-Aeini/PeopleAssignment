using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.Services;

namespace PeopleAssignment.Controllers
{
    public class AjaxController : Controller
    {
        private IPeopleService Service { get; }

        public AjaxController(PeopleService service)
        {
            Service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return PartialView(Service.All());
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            Person person = Service.FindById(id);

            if (person == null)
            {
                return NotFound();
            }

            return PartialView(person);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Person person = Service.FindById(id);

            if (person == null)
            {
                return NotFound();
            }

            Service.Remove(id);

            return Ok();
        }
    }
}
