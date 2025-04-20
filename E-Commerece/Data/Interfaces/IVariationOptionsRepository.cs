using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IVariationOptionsRepository
    {
        List<VariationOption> GetVariationOptionsByVariationId(int variationid);
        void DeleteVariationOptionsByVariationId(int variationid);
        VariationOption GetVariationOptionById(int variationid);
        void DeleteVariationOption(VariationOption variationOption);
        void CreateVariationOption(VariationOption variationOption);
        VariationOption GetByValueAndVariation(string Value, int Variationid);
        void EditVariationOption(string value, int optionid);
    }
}
