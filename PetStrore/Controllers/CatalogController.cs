using Microsoft.AspNetCore.Mvc;
using PetStrore.Models;
using PetStrore.Services;

namespace PetStrore.Controllers
{
    public class CatalogController : Controller
    {
        IDataService service;
        public CatalogController(IDataService service)
        {
            this.service = service;
        }
        public IActionResult Catalog(string category)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            var animals = service.GetAnimalsByCategoryAsync(category).Result;
            return View(animals);
        }
        public IActionResult ViewAnimal(int animalId)
        {
            var animal = service.GetAnimalByIdAsync(animalId).Result;
            return View(animal);
        }
        public IActionResult AddComment(string commentText,int animalId)
        {
            var animal = service.GetAnimalByIdAsync(animalId).Result;
            service.InsertComment(commentText, animal);
            service.SaveChanges();
            return View("ViewAnimal", animal);
        }
        public IActionResult Adoption(int animalId)
        {
            return View(service.GetAnimalByIdAsync(animalId).Result);
        }
    }
}
