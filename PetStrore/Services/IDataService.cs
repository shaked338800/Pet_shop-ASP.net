using PetStrore.Models;

namespace PetStrore.Services
{
    public interface IDataService
    {
        public Task<IEnumerable<Animal>> GetAnimalsAsync();
        public Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(string category);
        public Task<Animal> GetAnimalByIdAsync(int id);
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<bool> AdminValidationAsync(string userName, string password);
        public Task<IEnumerable<Animal>> GetPopularAnimalsAsync();
        public void InsertCommentAsync(string commentText, Animal animal);
        public void InsertAnimal(Animal animal);
        public void InsertComment(string commentText, Animal animal);
        public void DeleteAnimal(Animal animal);
        public bool UpdateAnimal(Animal animal);
        public void InsertAdmin(Admin admin);      
        public void InsertCategory(Category category);
        public void DeleteCategory(Category category);
        public Task<Category> GetCategoryByIdAsync(int categoryId);
        public void SaveChanges();
    
    }
}
