using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommereceContext _context;
        public CategoryRepository(EcommereceContext context)
        {
            this._context = context;
        }

        public void Create(Category category)
        {
            this._context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            category.IsActive = false;
        }

        public List<Category> GetAllCategories() => this._context.Categories.Include(x => x.Products).Where(x => x.IsActive).ToList();

        public Category GetCategory(int id) => this._context.Categories
            .Include(x => x.Variations)
            .ThenInclude(x => x.VariationOptions)
            .SingleOrDefault(x => x.Id == id);
    }
}
