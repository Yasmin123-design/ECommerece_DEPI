using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface IVariationService
    {
        List<Variation> GetVariationByCategoryId(int categoryId);
        List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds);
        void SaveChange();
    }
}
