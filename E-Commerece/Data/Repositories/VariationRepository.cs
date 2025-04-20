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

        public void AddListOfVariationsAndThierOptions(List<Variation> variations,int categoryId)
        {
            this._context.Variations.AddRange(variations);
            foreach (var variation in variations)
            {
                variation.CategoryId = categoryId;
                foreach(var option in variation.VariationOptions)
                {
                    option.VariationId = variation.Id;
                }
                this._context.VariationOptions.AddRange(variation.VariationOptions);
            }
            
        }

        public void AddVariation(Variation variation)
        {
            this._context.Variations.Add(variation);
        }

        public void DeleteVariationById(int variationId)
        {
            var variation = GetVariationById(variationId);
            this._context.Variations.Remove(variation);
        }

        public void DeleteVariationsByCategoryId(int categoryId)
        {
            var variations = GetVariationByCategoryId(categoryId);
            this._context.Variations.RemoveRange(variations);
        }

        public void EditVariation(int id, string name)
        {
            var variation = GetVariationById(id);
            variation.Name = name;
        }

        public List<Variation> GetVariationByCategoryId(int categoryId) => this._context.Variations.Include(x => x.VariationOptions).Where(x => x.CategoryId == categoryId).ToList();

        public Variation GetVariationById(int variationId) => this._context.Variations.FirstOrDefault(x => x.Id == variationId);

        public Variation GetVariationByNameAndCategory(string name, int categoryId) => _context.Variations
        .FirstOrDefault(v => v.Name == name && v.CategoryId == categoryId);

        public List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds) => this._context.VariationOptions.Where(x => variationOptionIds.Contains(x.Id)).ToList();
    }
}
