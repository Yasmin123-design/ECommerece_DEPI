using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class VariationOptionService : IVariationOptionsService
    {

        private readonly IUnitOfWork _unitOfWork;
        public VariationOptionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void CreateVariationOption(VariationOption variationOption)
        {
            this._unitOfWork.VariationOptions.CreateVariationOption(variationOption);
        }

        public void DeleteVariationOption(VariationOption variationOption)
        {
            this._unitOfWork.VariationOptions.DeleteVariationOption(variationOption);
        }

        public void DeleteVariationOptionsByVariationId(int variationid)
        {
            this._unitOfWork.VariationOptions.DeleteVariationOptionsByVariationId(variationid);
        }

        public void EditVariationOption(string value, int optionid)
        {
            this._unitOfWork.VariationOptions.EditVariationOption(value, optionid);
        }

        public VariationOption GetByValueAndVariation(string value, int variationId) => this._unitOfWork.VariationOptions.GetByValueAndVariation(value, variationId);

        public VariationOption GetVariationOptionById(int variationid) => 
            this._unitOfWork.VariationOptions.GetVariationOptionById(variationid);
        

        public void SaveChange()
        {
            this._unitOfWork.Save();
        }

        public void UpdateVariationOption(VariationOption existingOption, VariationOption option)
        {
            this._unitOfWork.VariationOptions.UpdateVariationOption(existingOption, option);
        }
    }
}
