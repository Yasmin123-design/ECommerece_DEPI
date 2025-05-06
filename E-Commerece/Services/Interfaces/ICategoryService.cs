using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        void Create(Category category);
        Category GetCategory(int id);
        void Delete(Category category);
        int GetCategoryCount();
        void SaveChange();
    }
}
