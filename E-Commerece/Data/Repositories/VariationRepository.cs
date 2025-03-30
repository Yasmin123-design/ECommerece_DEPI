using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.Data.Repositories
{
    public class VariationRepository : IVariationRepository
    {
        private readonly EcommereceContext _context;
        public VariationRepository(EcommereceContext context)
        {
            this._context = context;
        }
        public List<Variation> GetVariationByCategoryId(int categoryId) => this._context.Variations.Include(x => x.VariationOptions).Where(x => x.CategoryId == categoryId).ToList();

        public List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds) => this._context.VariationOptions.Where(x => variationOptionIds.Contains(x.Id)).ToList();
    }
}
