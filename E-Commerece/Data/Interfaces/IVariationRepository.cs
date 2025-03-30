using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IVariationRepository
    {
        List<Variation> GetVariationByCategoryId(int categoryId);
        List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds);
    }
}
