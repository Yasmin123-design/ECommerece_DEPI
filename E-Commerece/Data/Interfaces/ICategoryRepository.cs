using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategory(int id);
        void Create(Category category);
        void Delete(Category category);

        int GetCategoryCount();
    }
}
