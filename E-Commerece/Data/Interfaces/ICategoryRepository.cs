using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}
