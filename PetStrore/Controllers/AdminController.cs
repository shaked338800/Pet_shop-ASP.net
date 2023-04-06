using Microsoft.AspNetCore.Mvc;
using PetStrore.Models;
using PetStrore.Services;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.IO;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetStrore.Controllers
{
    public class AdminController : Controller
    {
        IDataService service;
        public AdminController(IDataService service)
        {
            this.service = service;
            
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginValidation(string username, string password)
        {
            if (service.AdminValidationAsync(username, password).Result)
                return RedirectToAction("AdminView", new { admin = true });
            else
                return RedirectToAction("Login");
        }
        public IActionResult AdminView(bool admin)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result; 
            ViewBag.Animals = service.GetAnimalsAsync().Result;
            if (admin)
                return View();
            else
                return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                service.InsertAnimal(animal);
                service.SaveChanges();
                return RedirectToAction("AdminView", new { admin = true });
            }
            else
            {
                ViewBag.Categories = service.GetCategoriesAsync().Result;
                return View("AddAnimal");
            }
        }
        [HttpGet]
        public IActionResult AddAnimal(bool admin)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            if (admin)
                return View();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult DeleteAnimal(int animalId)
        {
            var animal = service.GetAnimalByIdAsync(animalId).Result;
            service.DeleteAnimal(animal);
            service.SaveChanges();
            return RedirectToAction("AdminView", new { admin = true });
        }
        [HttpGet]
        public IActionResult DeleteAnimalPage(string category, bool admin)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            var animal = service.GetAnimalsByCategoryAsync(category).Result;
            if (admin)
                return View(animal);
            return Redirect("Login");
        }
        [HttpGet]
        public IActionResult EditAnimal(int animalId)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            var animal = service.GetAnimalByIdAsync(animalId).Result;
            return View(animal);
        }
        [HttpPost]
        public IActionResult EditAnimal(Animal animal)
        {
            var animalDb = service.GetAnimalByIdAsync(animal.AnimalId).Result;
            if (ModelState.IsValid)
            {
                service.UpdateAnimal(animal);
                service.SaveChanges();
                return RedirectToAction("AdminView", new { admin = true });
            }
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            return View("EditAnimal", animalDb);
        }
        [HttpGet]
        public IActionResult EditAnimalPage(string category, bool admin)
        {
            ViewBag.Categories = service.GetCategoriesAsync().Result;
            var animal = service.GetAnimalsByCategoryAsync(category).Result;
            if (admin)
                return View(animal);
            return Redirect("Login");
        }
        [HttpGet]
        public IActionResult AddAdmin(bool admin)
        {
            if (admin)
                return View();
            return Redirect("Login");
        }
        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                service.InsertAdmin(admin);
                service.SaveChanges();
                return RedirectToAction("AdminView", new { admin = true });
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddCategory(bool admin)
        {
            if (admin)
                return View();
            return Redirect("Login");
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                service.InsertCategory(category);
                service.SaveChanges();
                return RedirectToAction("AdminView", new { admin = true });
            }
            return View();
        }
        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = service.GetCategoryByIdAsync(categoryId).Result;
            service.DeleteCategory(category);
            service.SaveChanges();
            return RedirectToAction("AdminView", new { admin = true });
        }
        [HttpGet]
        public IActionResult DeleteCategoryPage(bool admin)
        {
            var categories = service.GetCategoriesAsync().Result;
            if (admin)
                return View(categories);
            return Redirect("Login");
        }
    }
}
