using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class VariationService : IVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VariationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public List<Variation> GetVariationByCategoryId(int categoryId) => this._unitOfWork.Variations.GetVariationByCategoryId(categoryId);

        public List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds) => this._unitOfWork.Variations.GetVariationOptionsByIds(variationOptionIds);
        public void SaveChange()
        {
            this._unitOfWork.Save();
        }
    }
}
