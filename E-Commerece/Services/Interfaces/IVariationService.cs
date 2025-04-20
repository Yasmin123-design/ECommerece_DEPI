using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface IVariationService
    {
        List<Variation> GetVariationByCategoryId(int categoryId);
        List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds);
        void AddListOfVariationsAndThierOptions(List<Variation> variations , int categoryId);
        void DeleteVariationsByCategoryId(int categoryId);
        void DeleteVariationById(int variationId);
        void AddVariation(Variation variation);
        Variation GetVariationById(int variationId);
        Variation GetVariationByNameAndCategory(string name, int categoryId);
        void EditVariation(int id, string name);
        void SaveChange();
    }
}
