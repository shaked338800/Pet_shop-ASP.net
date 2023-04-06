using Microsoft.EntityFrameworkCore;
using PetStrore.Data;
using PetStrore.Models;

namespace PetStrore.Services
{
    public class DataService : IDataService
    {
        ShopDbContext context;
        public DataService(ShopDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Animal>> GetAnimalsAsync() => await Task.Run(() => GetAnimals());
        public async Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(string category) => await Task.Run(() => GetAnimalsByCategory(category));
        public async Task<Animal> GetAnimalByIdAsync(int id) => await Task.Run(() => GetAnimalById(id));
        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await Task.Run(() => GetCategories());
        public async Task<bool> AdminValidationAsync(string userName, string password) => await Task.Run(() => AdminValidation(userName, password));
        public async Task<IEnumerable<Animal>> GetPopularAnimalsAsync() => await Task.Run(() => GetPopularAnimals());
        public Task<Category> GetCategoryByIdAsync(int categoryId) => Task.Run(() => GetCategoryById(categoryId));
        public void InsertCommentAsync(string commentText, Animal animalId) => Task.Run(() => InsertComment(commentText, animalId));
        public void InsertAnimal(Animal animal)
        {
           // animal.Category = context.Categories!.FirstOrDefault(c => c.CategoryId == animal.CategoryId);
            context.Animals!.Add(animal);
        }
        public void InsertComment(string commentText, Animal animal) => context.Comments!.Add(new Comment { Text = commentText, Animal = animal, AnimalId = animal.AnimalId });
        public void DeleteAnimal(Animal animal) => context.Animals!.Remove(animal);
        public bool UpdateAnimal(Animal animal)
        {
            var animalInDb = context.Animals!.SingleOrDefault(a => a.AnimalId == animal.AnimalId);
            if(animalInDb == null) return false;
            if(animalInDb!.Name != animal.Name)animalInDb.Name = animal.Name;
            if(animalInDb!.Age != animal.Age)animalInDb.Age = animal.Age;
            if(animalInDb!.ImageUrl != animal.ImageUrl) animalInDb.ImageUrl = animal.ImageUrl;
            if(animalInDb!.Description != animal.Description) animalInDb.Description = animal.Description;
            if(animalInDb!.CategoryId != animal.CategoryId) animalInDb.CategoryId = animal.CategoryId;
            return true;
        }
        public void SaveChanges() => context.SaveChanges();
        public void InsertAdmin(Admin admin) => context.Admins!.Add(admin);
        public void InsertCategory(Category category) => context.Categories!.Add(category);
        public void DeleteCategory(Category category) => context.Categories!.Remove(category);

        IEnumerable<Animal> GetAnimals() => context.Animals!;
        Animal GetAnimalById(int id) => context.Animals!.Where(a => a.AnimalId == id).First();
        IEnumerable<Animal> GetAnimalsByCategory(string category)
        {
            if (String.IsNullOrEmpty(category)) return context.Animals!;
            else
            {
                return context.Animals!.Where(a => a.Category!.Name == category);
            }
        }
        IEnumerable<Category> GetCategories() => context.Categories!;
        IEnumerable<Animal> GetPopularAnimals() => context.Animals!.OrderByDescending(a => a.Comments!.Count).Take(2);
        bool AdminValidation(string userName, string password) => context.Admins!.FirstOrDefault(a => a.Password == password && a.UserName == userName) == default(Admin) ? false : true;
        Category GetCategoryById(int categoryId) => context.Categories!.Where(c => c.CategoryId == categoryId).First();
    }
}
