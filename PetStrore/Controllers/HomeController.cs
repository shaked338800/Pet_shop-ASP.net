using Microsoft.AspNetCore.Mvc;
using PetStrore.Data;
using PetStrore.Services;

namespace PetStrore.Controllers
{
    public class HomeController : Controller
    {
        IDataService service;
        public HomeController(IDataService service)
        {
            this.service = service;
        }
        public IActionResult HomePage()
        {
            var popularAnimals = service.GetPopularAnimalsAsync().Result;
            return View(popularAnimals);
        }

        public IActionResult ContactUsPage()
        {
            return View();
        }

    }
}
