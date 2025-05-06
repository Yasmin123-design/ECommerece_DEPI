using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface IVariationOptionsService
    {
        void DeleteVariationOptionsByVariationId(int variationid);
        VariationOption GetVariationOptionById(int variationid);
        void DeleteVariationOption(VariationOption variationOption);
        void CreateVariationOption(VariationOption variationOption);
        VariationOption GetByValueAndVariation(string value, int variationId);
        void EditVariationOption(string value, int optionid);
        void UpdateVariationOption(VariationOption existingOption, VariationOption option);
        void SaveChange();
    }
}
