using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Create(Category category)
        {
            this._unitOfWork.Categories.Create(category);
        }

        public void SaveChange()
        {
            this._unitOfWork.Save();
        }
        public List<Category> GetAllCategories() => this._unitOfWork.Categories.GetAllCategories();

        public Category GetCategory(int id) => this._unitOfWork.Categories.GetCategory(id);

        public void Delete(Category category)
        {
            this._unitOfWork.Categories.Delete(category);
        }

        public int GetCategoryCount() => this._unitOfWork.Categories.GetCategoryCount();
    }
}
