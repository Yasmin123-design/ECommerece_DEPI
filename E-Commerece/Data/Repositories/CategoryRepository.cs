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

        public List<Category> GetAllCategories() => this._context.Categories.Include(x => x.Products).ToList();
    }
}
