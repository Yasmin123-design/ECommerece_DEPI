using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IVariationRepository
    {
        List<Variation> GetVariationByCategoryId(int categoryId);
        List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds);

        void AddListOfVariationsAndThierOptions(List<Variation> variations , int categoryId);
        void DeleteVariationsByCategoryId(int categoryId);
        void DeleteVariationById(int variationId);

        Variation GetVariationById(int variationId);
        void AddVariation(Variation variation);
        Variation GetVariationByNameAndCategory(string name, int categoryId);
        void EditVariation(int id, string name);
    }
}
